using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        _houseAnim = _house.GetComponent<Animator>();
    }

    public void increaseGeneration()
    {
        _currentGeneration += 1;
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

    public void InsideTheHouse()
    {
        _isInsideHouse = true;
        _houseAnim.SetTrigger(EnterHouse);
        _enterHouseBtn.gameObject.SetActive(false);
        _exitHouseBtn.gameObject.SetActive(true);
        for (int i = 0; i < _objectsToFind.Count; i++)
        {
            _objectsToFind[i].gameObject.SetActive(i == _currentGeneration);
        }
    }

    public void OutsideTheHouse()
    {
        _isInsideHouse = false;
        _houseAnim.SetTrigger(ExitHouse);
        _enterHouseBtn.gameObject.SetActive(true);
        _exitHouseBtn.gameObject.SetActive(false);
        
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
