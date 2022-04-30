using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class GenerationManager : Singleton<GenerationManager>
{
    [Header("UI elements")] 
    [SerializeField] private Button _enterHouseBtn;
    [SerializeField] private Button _exitHouseBtn;
    [Header("house and gen objects")]
    [SerializeField] private int _currentGeneration = 0;
    [SerializeField] private GameObject _house;
    [SerializeField] private GameObject _InsideofHouse;
    [SerializeField] private GameObject _outsideOfHouse;
    [SerializeField] private List<GameObject> _objectsToFind;
    [SerializeField] private List<GameObject> _mushroomPeople;
    private bool _canFindMush;
    private Animator _houseAnim;
    private static readonly int EnterHouse = Animator.StringToHash("enterHouse");
    private static readonly int ExitHouse = Animator.StringToHash("exitHouse");
    private bool _isInsideHouse;
    private bool _firstTimeInside;
    [SerializeField] private int _numOfTimesSuceeded;
    [SerializeField] private List<bool> _didPlayerSucceed;
    [SerializeField] private List<GameObject> seeds;
    [SerializeField]  private List<GameObject> trash;
    private int n;

    private void Start()
    {
        n = 0;
        _houseAnim = _house.GetComponent<Animator>();
        _isInsideHouse = false;
    }

    public void hasPlayerSuceeded(bool yesSuceeded)
    {
        _didPlayerSucceed[n] = yesSuceeded;
        increaseGeneration();
        _firstTimeInside = false;
        if (yesSuceeded)
        {
            seeds[n].SetActive(true);
            _numOfTimesSuceeded++;
        }
        else
        {
            trash[n].SetActive(true);
        }

        n++;
        JournalManager.Instance.NextGeneration(_currentGeneration);
    }

    private void Update()
    {
        if (_isInsideHouse)
        {
            _enterHouseBtn.gameObject.SetActive(false);
            _exitHouseBtn.gameObject.SetActive(true);
        }
        else
        {
            _enterHouseBtn.gameObject.SetActive(true);
            _exitHouseBtn.gameObject.SetActive(false);
        }
    }

    public void increaseGeneration()
    {
        _currentGeneration += 1;
        MouseManager.Instance.currentFlowchart = MouseManager.Instance.FlowchartsInGame[_currentGeneration];

        foreach (Flowchart fC in MouseManager.Instance.FlowchartsInGame)
        {
            fC.gameObject.SetActive(false);
        }
        MouseManager.Instance.currentFlowchart.gameObject.SetActive(true);
    }

    public int ReturnGeneration()
    {
        return _currentGeneration;
    }

    public void ToggleMushFind(bool canFind)
    {
        _canFindMush = canFind;
    }

    public void toggleBtnsOn(bool turnOff)
    {
        if (turnOff)
        {
            _enterHouseBtn.gameObject.SetActive(false);
            _exitHouseBtn.gameObject.SetActive(false);
        }
        else
        {
            if (_InsideofHouse)
            {
                _enterHouseBtn.gameObject.SetActive(false);
                _exitHouseBtn.gameObject.SetActive(true);
            }
            else
            {
                _enterHouseBtn.gameObject.SetActive(true);
                _exitHouseBtn.gameObject.SetActive(false);
            }
        }
    }

    public void CallWhenInside()
    {
        MouseManager.Instance.currentFlowchart.ExecuteBlock("FirstInsideHouse");
    }
    
    public void InsideTheHouse()
    {
        if (!_firstTimeInside)
        {
            _firstTimeInside = true;
            CallWhenInside();
        }

        _isInsideHouse = true;
        _houseAnim.SetTrigger(EnterHouse);
        for (int i = 0; i < _objectsToFind.Count; i++)
        {
            _objectsToFind[i].gameObject.SetActive(i == _currentGeneration);
        }
    }

    public void OutsideTheHouse()
    {
        _isInsideHouse = false;
        _houseAnim.SetTrigger(ExitHouse);
        
        if (_canFindMush)
        {
            for (int i = 0; i < _mushroomPeople.Count; i++)
            {
                _mushroomPeople[i].gameObject.SetActive(i == _currentGeneration);
            }
        }
        else
        {
            foreach (GameObject mushPerson in _mushroomPeople)
            {
                mushPerson.gameObject.SetActive(false);
            }
        }
    }
}
