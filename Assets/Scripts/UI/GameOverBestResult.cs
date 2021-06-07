using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverBestResult : MonoBehaviour
{
    private Text _text;

    private string _baseText = "";

    private void Awake() => _text = GetComponent<Text>();

    public void Init(string baseText)
    {
        _baseText = baseText;
    }

    public void ChangeBestResult(int value)
    {
        _text.text = _baseText + value.ToString();
    }
}
