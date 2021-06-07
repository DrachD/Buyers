using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    public Action<ButtonType> OnClickChoiceFigureEvent;

    public ButtonType ButtonType;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate{SetFigure(ButtonType);});
    }

    public void SetFigure(ButtonType buttonType)
    {
        OnClickChoiceFigureEvent?.Invoke(buttonType);
    }
}
