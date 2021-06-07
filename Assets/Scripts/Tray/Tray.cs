using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    public List<GameObject> figures = new List<GameObject>();
    public List<FigureType> figureTypes = new List<FigureType>();

    private FigureOnTray[] figuresOnTray;

    public int maxCountFigures = 3;

    public int currentCountFigures = 0;

    private void Awake()
    {
        figuresOnTray = GetComponentsInChildren<FigureOnTray>();
    }

    ///<summary>
    /// put the necessary figures on the tray
    ///</summary>
    public void PutFigureOnTray(GameObject figure)
    {
        if (currentCountFigures >= maxCountFigures)
            return;

        figures.Add(figure);
        figureTypes.Add(figure.GetComponent<Figure>().FigureType);

        
        GameObject obj = Instantiate(figure, figuresOnTray[currentCountFigures].transform.position, Quaternion.identity);
        obj.transform.SetParent(transform);

        currentCountFigures++;
    }
}
