using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllSort;

public class Human : MonoBehaviour
{
    [SerializeField] FloatVariable _moveSpeed;

    [SerializeField] FloatVariable _maxWaitingTime;

    [SerializeField] FloatVariable _minWaitingTime;

    [SerializeField] FigureBar _figureBar;

    [SerializeField] HealthBar _healthBar;

    private Sprite[] _figureSprites;

    public FigureType[] figureTypes;

    private Transform startPosition;

    private bool start = true;

    private int id;

    private HumanController _humanController;

    public bool isHaveTray = false;

    private float moveSpeed;

    private float lastWaypointSwitchTime;

    private Transform endPosition;

    private float maxWaitingTime;

    private float minWaitingTime;

    private void Start()
    {
        moveSpeed = _moveSpeed.value;
        maxWaitingTime = _maxWaitingTime.value;
        minWaitingTime = _minWaitingTime.value;
        Sort.SortAscending(figureTypes);
        lastWaypointSwitchTime = Time.time;
        StartCoroutine(MoveEnemy());
    }

    ///<summary>
    /// This coroutine moves the buyer to the specified points from and to
    ///</summary>
    IEnumerator MoveEnemy()
    {
        while (true)
        {
            Vector3 targetPosition;

            if (start)
            {
                targetPosition = endPosition.position;

                if (transform.position == targetPosition)
                {
                    start = false;
                    _humanController.HumanNearConveyor(id, true);

                    float waitingTime = Random.Range(minWaitingTime, maxWaitingTime);
                    yield return WaitingTime(waitingTime);
                    
                    _humanController.HumanNearConveyor(id, false);
                    _humanController.OnUpdateScoreEvent(isHaveTray);
                }
            }
            else
            // Will start moving in the opposite direction
            {
                targetPosition = startPosition.position;

                if (transform.position == targetPosition)
                {
                    _humanController.DestroyHuman(id);
                    Destroy(gameObject);
                }
            }

            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);

            yield return null;
        }
    }

    ///<summary>
    /// This coroutine is needed to wait for time and update time in the UI
    ///</summary>
    IEnumerator WaitingTime(float time)
    {
        float _leftTime = time;

        while (!isHaveTray)
        {
            _leftTime -= Time.deltaTime;

            // update time in the UI Panel
            _healthBar.UpdateTimeUI(_leftTime / time);

            if (_leftTime <= 0f)
            {
                yield break;
            }

            yield return null;
        }
    }

    ///<summary>
    /// initialization of the required initial values
    ///</summary>
    public void Init(HumanController humanController, FigureType[] figureTypes, Sprite[] figureSprites, int id)
    {
        _humanController = humanController;
        startPosition = _humanController.points[id].startPosition;
        endPosition = _humanController.points[id].endPosition;
        this.id = id;
        _figureSprites = figureSprites;

        // Fill the bar panel with the necessary figures
        _figureBar.FillFigureIntoBar(_figureSprites);
        this.figureTypes = figureTypes;
    }
}
