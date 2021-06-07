using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image timerBar;

    public void UpdateTimeUI(float time)
    {
        timerBar.fillAmount = time;
    }
}
