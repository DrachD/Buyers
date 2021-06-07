using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AllSort;

public class MoveTray : MonoBehaviour
{
    [SerializeField] GameObject[] _waypoints;

    private int _currentWaypoint;

    private float lastWaypointSwitchTime;

    public float speed = 5f;

    private Game _game;

    private Transform[] _checkPoints;

    private int _checkPointIndex = 0;

    private Tray _tray;
    public Tray Tray => _tray;

    private void Awake()
    {
        _tray = GetComponent<Tray>();
    }

    private void Start()
    {
        _currentWaypoint = 0;
    }

    public void TrayLaunch(Game game)
    {
        _game = game;
        _checkPoints = game.checkPoints;
        Sort.SortAscending(_tray.figureTypes);
        lastWaypointSwitchTime = Time.time;
        StartCoroutine(MovementAlongPath());
    }

    ///<summary>
    /// we move the tray from the beginning of the path to the end of the path. 
    /// At each point where the buyer will be, we check if the figures coincide with the figures preferred by the buyer
    ///</summary>
    IEnumerator MovementAlongPath()
    {
        while (true)
        {
            Vector2 startPosition = _waypoints[_currentWaypoint].transform.position;
            Vector2 endPosition = _waypoints[_currentWaypoint + 1].transform.position;

            float pathLength = Vector2.Distance(startPosition, endPosition);

            float totalTimeForPath = pathLength / speed;
            float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
            float fractionOfJourney = currentTimeOnPath / totalTimeForPath;

            transform.position = Vector2.Lerp(startPosition, endPosition, fractionOfJourney);

            if (transform.position.Equals(endPosition))
            {
                // we check at all points of buyers
                if (_currentWaypoint >= 0 && _currentWaypoint < _waypoints.Length - 2)
                {
                    Debug.Log(_currentWaypoint);
                    bool gameOver = _game.CheckHuman(_currentWaypoint, this);
                    if (gameOver)
                    {
                        yield break;
                    }
                }

                if (_currentWaypoint < _waypoints.Length - 2)
                {
                    _currentWaypoint++;
                    lastWaypointSwitchTime = Time.time;
                }
                else
                {
                    _game.UpdateScore(false);
                    Destroy(gameObject);
                }
            }

            yield return null;
        }
    }
}
