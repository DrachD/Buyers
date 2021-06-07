using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    private IntVariable _bestResult;

    [SerializeField] IntVariable _amountOfPointsCorrectly;

    [SerializeField] IntVariable _amountOfPointsWrong;

    [SerializeField] GameObject _trayPrefab;

    [SerializeField] Transform _startingPointTray;

    [SerializeField] GameObject[] _figures;

    [SerializeField] InfoPanel _infoPanel;

    public Transform[] checkPoints;

    public int CountFigures => _figures.Length;

    public Dictionary<ButtonType, GameObject> figuresDictionary = new Dictionary<ButtonType, GameObject>();


    #region Tray Data
    private Tray tray;

    private MoveTray moveTray;

    #endregion
    private GameObject _tray;

    private HumanController _humanController;

    private int score = 0;

    private int amountOfPointsCorrectly;

    private int amountOfPointsWrong;

    private void Awake()
    {
        amountOfPointsCorrectly = _amountOfPointsCorrectly.value;
        amountOfPointsWrong = _amountOfPointsWrong.value;
        _humanController = GameObject.Find("HumanController").GetComponent<HumanController>();
        _bestResult = Resources.Load<IntVariable>("Save/BestResult");
        _humanController.OnUpdateScoreEvent += UpdateScore;
    }

    private void Start()
    {
        _infoPanel.OnChangeScoreEvent.Invoke(score);
        PutFiguresOnDictionary();

        TrayInstantiate();
    }

    ///<summary>
    /// Create a tray
    ///</summary>
    private void TrayInstantiate()
    {
        _tray = Instantiate(_trayPrefab, _startingPointTray.position, Quaternion.identity);
        tray = _tray.GetComponent<Tray>();
        moveTray = _tray.GetComponent<MoveTray>();
    }

    ///<summary>
    /// put the necessary figures on the tray by pressing a certain button
    ///</summary>
    private void TrayManipulation(ButtonType buttonType)
    {
        tray.PutFigureOnTray(figuresDictionary[buttonType]);
    }

    ///<summary>
    /// The method checks for the customer's pouring near the container. 
    /// If the customer is near him and the tray with figures matches the customer's preference, 
    /// then the customer takes the tray and leaves
    ///</summary>
    public bool CheckHuman(int id, MoveTray mTray)
    {
        // If the buyer exists and he is near the container
        if (_humanController.humans[id] != null &&
            _humanController.isNearConveyors[id])
        {
            Human human = _humanController.humans[id].GetComponent<Human>();

            int countFigureTypes_tray = mTray.Tray.figureTypes.Count;
            int countFigureTypes_human = human.figureTypes.Length;

            // if the number of figures preferred by the buyer matches the number of figures on the tray
            if (countFigureTypes_tray != countFigureTypes_human || human.isHaveTray)
            {
                return false;
            }

            // if there are figures on the tray that the buyer needs
            for (int i = 0; i < mTray.Tray.figureTypes.Count; i++)
            {
                if (human.figureTypes[i] != mTray.Tray.figureTypes[i])
                {
                    return false;
                }
            }
            mTray.gameObject.transform.SetParent(_humanController.humans[id].transform);
            human.isHaveTray = true;
            return true;
        }
        return false;
    }

    ///<summary>
    /// Update the number of points in the UI
    ///</summary>
    public void UpdateScore(bool isCorrectly)
    {
        score += (isCorrectly) ? amountOfPointsCorrectly : amountOfPointsWrong;
        _infoPanel.OnChangeScoreEvent.Invoke(score);
    }

    ///<summary>
    /// Start the tray by pressing the ok button
    ///<summary>
    private void ClickOKEvent()
    {
        moveTray.TrayLaunch(this);
        TrayInstantiate();
    }

    ///<summary>
    /// We put figures in the dictionary in order to get figures 
    /// from the dictionary by the type of figures
    ///</summary>
    private void PutFiguresOnDictionary()
    {
        for (int i = 0; i < _figures.Length; i++)
        {
            FigureType figureType = _figures[i].GetComponent<Figure>().FigureType;
            switch (figureType)
            {
                case FigureType.Circle:
                    figuresDictionary.Add(ButtonType.Circle, _figures[i]);
                    break;
                case FigureType.Scuare:
                    figuresDictionary.Add(ButtonType.Scuare, _figures[i]);
                    break;
                case FigureType.Cylindr:
                    figuresDictionary.Add(ButtonType.Cylindr, _figures[i]);
                    break;
            }
        }
    }

    ///<summary>
    /// transfer the button type to the TrayManipulation
    ///</summary>
    public void PutFigure(ButtonType buttonType)
    {
        switch (buttonType)
        {
            case ButtonType.Circle:
                TrayManipulation(ButtonType.Circle);
                break;
            case ButtonType.Scuare:
                TrayManipulation(ButtonType.Scuare);
                break;
            case ButtonType.Cylindr:
                TrayManipulation(ButtonType.Cylindr);
                break;
            case ButtonType.Ok:
                ClickOKEvent();
                break;
        }
    }
    
    /// <summary>
    /// At the end of the game, 
    /// we display the best result on the screen and 
    /// the number of points at the current moment
    ///</summary>
    public void GameOver(Action<int> scoreEvent, Action<int> bestResultEvent)
    {
        if (score > _bestResult.value)
        {
            _bestResult.value = score;
        }

        scoreEvent.Invoke(score);
        bestResultEvent.Invoke(_bestResult.value);
    }
}
