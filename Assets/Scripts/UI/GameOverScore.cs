using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    private Text _text;

    private string _baseText = "";

    private void Awake() => _text = GetComponent<Text>();

    public void Init(string baseText)
    {
        _baseText = baseText;
    }

    public void ChangeScoreResult(int value)
    {
        _text.text = _baseText + value.ToString();
    }
}
