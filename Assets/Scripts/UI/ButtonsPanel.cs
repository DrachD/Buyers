using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonsPanel : MonoBehaviour
{
    public Action OnClickOkEvent;

    [SerializeField] ButtonUI[] _buttons; 

    [SerializeField] Game game;

    private void OnValidate()
    {
        _buttons = GetComponentsInChildren<ButtonUI>();
    }

    private void Awake()
    {
        game = GameObject.Find("Game").GetComponent<Game>();

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].OnClickChoiceFigureEvent += game.PutFigure;
        }
    }
}

public enum ButtonType
{
    Circle,
    Scuare,
    Cylindr,
    Ok
}
