using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PanelGameOver : MonoBehaviour
{
    public Action<int> OnChangeBestResultEvent;

    public Action<int> OnChangeScoreResultEvent;

    [SerializeField] string _baseTextScore;

    [SerializeField] string _baseTextBestResult;

    [SerializeField] GameOverBestResult _gameOverBestResult;

    [SerializeField] GameOverScore _gameOverScore;

    private void Awake()
    {
        _gameOverBestResult = GetComponentInChildren<GameOverBestResult>();
        _gameOverScore = GetComponentInChildren<GameOverScore>();
        
        OnChangeBestResultEvent += _gameOverBestResult.ChangeBestResult;
        OnChangeScoreResultEvent += _gameOverScore.ChangeScoreResult;

        _gameOverScore.Init(_baseTextScore);
        _gameOverBestResult.Init(_baseTextBestResult);

        gameObject.SetActive(false);
    }
    
}
