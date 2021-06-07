using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] Game _game;

    [SerializeField] InfoPanel _infoPanel;

    [SerializeField] IntVariable _duration;

    [SerializeField] PanelGameOver _panelGameOver;

    private int _startTime;

    private float startTime;

    private float _elapsedTime;

    private int _lastTime = 0;

    private bool isPause = false;

    private void Start()
    {
        // launch game
        Time.timeScale = 1;

        _elapsedTime = 0;
        _startTime = _duration.value;
        startTime = Time.time;
    }

    private void Update()
    {
        int integer = 0;
        int reminder = 0;

        _elapsedTime = Time.time - startTime;

        if (_startTime <= _elapsedTime)
        {
            // enable game over bar
            _panelGameOver.gameObject.SetActive(true);

            // display the required results on the game over panel
            _game.GameOver(_panelGameOver.OnChangeScoreResultEvent, 
                           _panelGameOver.OnChangeBestResultEvent);
            _startTime += _duration.value;

            // stop game
            Time.timeScale = 0;
        }

        if (_lastTime <= _elapsedTime)
        {
            int value = _startTime - _lastTime;

            integer = value / 60;
            reminder = value % 60;

            // Update UI Time
            _infoPanel.OnChangeTimeEvent.Invoke(integer, reminder);
            _lastTime++;
        }

    }
}
