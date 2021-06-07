using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FigureData
{
    public FigureType figureType;
    public Sprite sprite;
}

[CreateAssetMenu]
public class Figures : ScriptableObject
{
    public FigureData[] figureDatas;
}
