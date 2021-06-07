using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Points
{
    public Transform startPosition;
    public Transform endPosition; 
}

public class HumanController : MonoBehaviour
{
    public Action<bool> OnUpdateScoreEvent;

    [SerializeField] Figures _figures;

    [SerializeField] GameObject _humanPrefab;

    [SerializeField] FloatVariable _timeUpdateByers;

    public Points[] points; 

    // we keep all created byers
    public GameObject[] humans;

    // true - if the buyer is near the conveyor
    public bool[] isNearConveyors;

    private int countHumans = 3;

    private int _waypointIndex = 0;

    private bool[] isTrackBusy;

    private Game _game;

    #region Figure Sprites and Figure Types
    private Sprite[] _figureSprites;
    private FigureType[] _figureTypes;
    #endregion

    private float timeUpdateByers;

    private void Awake()
    {
        timeUpdateByers = _timeUpdateByers.value;
        _game = GameObject.Find("Game").GetComponent<Game>();
        isTrackBusy = new bool[countHumans];
        isNearConveyors = new bool[countHumans];
        humans = new GameObject[countHumans];
    }

    private void Start()
    {
        StartCoroutine(MoveEnemy());
    }

    ///<summary>
    /// We select a path for a person every n seconds, 
    /// if any path is free, we spawn a person on it, 
    /// we also store all existing people in an array for manipulating food
    ///</summary>
    IEnumerator MoveEnemy()
    {
        while (true)
        {
            // store all numbers of free tracks in an array
            List<int> freeTrackNumbers = new List<int>();

            int countOfFreeTracks = 0;

            int trackNumbered = 0;

            // count the number of free tracks
            for (int i = 0; i < isTrackBusy.Length; i++)
            {
                if (isTrackBusy[i] == false)
                {
                    countOfFreeTracks++;
                    freeTrackNumbers.Add(i + 1);
                }
            }

            // if the number of free tracks is more than 0, then go ahead
            if (countOfFreeTracks > 0)
            {
                // choose a random track for the buyer
                trackNumbered = freeTrackNumbers[UnityEngine.Random.Range(0, freeTrackNumbers.Count)];
            }
            
            // if the random track is not 0, then create a person on this track
            if (trackNumbered > 0)
            {
                SetFigures(ref _figureSprites, ref _figureTypes);
                isTrackBusy[trackNumbered - 1] = true;
                // we will use the pool of objects in order not to create visitors on a permanent basis
                if (humans[trackNumbered - 1] == null)
                {
                    humans[trackNumbered - 1] = Instantiate(_humanPrefab, points[trackNumbered - 1].startPosition.position, Quaternion.identity);
                }
                else
                {
                    humans[trackNumbered - 1].SetActive(true);
                    humans[trackNumbered - 1].transform.position = points[trackNumbered - 1].startPosition.position;
                }
                humans[trackNumbered - 1].GetComponent<Human>().Init(this, _figureTypes,_figureSprites, trackNumbered - 1);
            }

            yield return new WaitForSeconds(timeUpdateByers);
        }
    }

    ///<summary>
    /// a person has already approached the conveyour and is waiting for his order
    ///</summary>
    public void HumanNearConveyor(int id, bool isNearConveyor)
    {
        isNearConveyors[id] = isNearConveyor;
    }

    ///<summary>
    /// We disconnect the buyer as needed
    ///</summary>
    public void DestroyHuman(int id)
    {
        isTrackBusy[id] = false;
        // turn off as needed
        humans[id].SetActive(false);
    }

    ///<summary>
    /// The buyer will require any products from us. 
    /// We will add to the buyer any number of figures of different or the same type
    ///</summary>
    public void SetFigures(ref Sprite[] figureSprite, ref FigureType[] figureType)
    {
        int countFigures = CountsFigures();
        figureSprite = new Sprite[countFigures];
        figureType = new FigureType[countFigures];

        for (int i = 0; i < countFigures; i++)
        {
            ChoiceFigure(ref figureSprite[i], ref figureType[i]);
        }
    }
    
    ///<summary>
    /// How many figures the buyer wants to have
    ///</summary>
    public int CountsFigures()
    {
        return UnityEngine.Random.Range(0, _game.CountFigures) + 1;
    }

    ///<summary>
    /// which figure should be on any slot
    ///</summary>
    public void ChoiceFigure(ref Sprite figureSprite, ref FigureType figureType)
    {
        int value = UnityEngine.Random.Range(0, _game.CountFigures);

        figureSprite = _figures.figureDatas[value].sprite;
        figureType = _figures.figureDatas[value].figureType;
    }
}
