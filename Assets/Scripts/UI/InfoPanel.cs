using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfoPanel : MonoBehaviour
{
    public Action<int> OnChangeScoreEvent;

    public Action<int, int> OnChangeTimeEvent;

    [SerializeField] string _baseTextScore;

    [SerializeField] string _baseTextTime;

    [SerializeField] InfoScore _infoScore;

    [SerializeField] InfoTime _infoTime;

    private void Awake()
    {
        _infoScore = GetComponentInChildren<InfoScore>();
        _infoTime = GetComponentInChildren<InfoTime>();
        
        OnChangeScoreEvent += _infoScore.ChangeScore;
        OnChangeTimeEvent += _infoTime.ChangeTime;
    }

    private void Start()
    {
        _infoScore.Init(_baseTextScore);
        _infoTime.Init(_baseTextTime);
    }
}
