using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTime : MonoBehaviour
{
    private Text _text;

    private string _baseText = "";

    private void Awake() => _text = GetComponent<Text>();

    public void Init(string baseText)
    {
        _baseText = baseText;
    }

    ///<summary>
    /// Display time formatted
    ///</summary>
    public void ChangeTime(int integer, int remainder)
    {
        if (integer < 10 && remainder < 10)
        {
            _text.text = _baseText + "0" + integer.ToString() + ":" + "0" + remainder.ToString();
        }
        else if (integer < 10 && remainder >= 10)
        {
            _text.text = _baseText + "0" + integer.ToString() + ":" + remainder.ToString();
        }
        else if (integer >= 10 && remainder < 10)
        {
            _text.text = _baseText + integer.ToString() + ":" + "0" + remainder.ToString();
        }
        else if (integer >= 10 && remainder >= 10)
        {
            _text.text = _baseText + integer.ToString() + ":" + remainder.ToString();
        }
    }
}
