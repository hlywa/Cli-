using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Febucci;
using Febucci.UI;
using TMPro;

public class JournalManager : Singleton<JournalManager>
{
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private GameObject _journal;
    [SerializeField] private List<ObjectEntry> _entries;
    [SerializeField] private List<ObjectEntry> _entriesSeeds;
    [SerializeField] private List<Material> _materials;
    [SerializeField] private List<Material> _seedMaterials;
    private static readonly int Power = Shader.PropertyToID("_Power");
    private Animator _thisAnimator;
    private static readonly int Disappear = Animator.StringToHash("Disappear");
    private int _currentGen;
    private int _numbofEntries;


    public Sprite _g2;
    public Sprite _g3;
    
    
    private void Start()
    {
        
        _thisAnimator = GetComponent<Animator>();

        foreach (Material gmObj in _materials)
        {
                StartCoroutine(c_InkBlotTransition(gmObj, false));
        }
        foreach (Material gmObj in _seedMaterials)
        {
                StartCoroutine(c_InkBlotTransition(gmObj, false));
        }
        _journal.SetActive(false);
    }

    public void Update()
    {
        _currentGen = GenerationManager.Instance.ReturnGeneration();
    }

    public void changeEntryNumber(int numOfEntries)
    {
        _numbofEntries = numOfEntries;
    }

    public IEnumerator c_openJournal()
    {
        yield return new WaitForSeconds(0.6f);
        
        if (_currentGen == 1)
        {
            _journal.GetComponent<Animator>().SetTrigger("g1g2");
        }
        else if (_currentGen == 2)
        {
            _journal.GetComponent<Animator>().SetTrigger("g2g3"); 
        }
        
        
        if (_numbofEntries == 0)
        {
            
        }
        else if (_numbofEntries == 1)
        {
            _entries[_currentGen].showText();
            StartCoroutine(c_waitforAnimation(_materials[_currentGen], true));
        }
        else if (_numbofEntries == 2)
        {
            _entries[_currentGen].showText();
            _entriesSeeds[_currentGen].showText();

            StartCoroutine(c_waitforAnimation(_materials[_currentGen], true));
            StartCoroutine(c_waitforAnimation(_seedMaterials[_currentGen], true));
        }

    }
    
    public void OpenJournal()
    {
        _journal.SetActive(true);
        _currentGen = GenerationManager.Instance.ReturnGeneration();
        StartCoroutine(c_openJournal());
    }

    public void CloseJournal()
    {
        StartCoroutine(c_closeJournal());
    }

    public void NextGeneration(int _currentGene)
    {
        StartCoroutine(c_InkBlotTransition(_materials[_currentGene - 1], false));
        StartCoroutine(c_InkBlotTransition(_seedMaterials[_currentGene - 1], false));
        _entries[_currentGene - 1].DeleteText();
        _entriesSeeds[_currentGene - 1].DeleteText();
        _journal.GetComponent<Animator>().ResetTrigger(Disappear);

    }
    private IEnumerator c_closeJournal()
    {
        yield return new WaitForSeconds(0.5f);
        _journal.GetComponent<Animator>().SetTrigger(Disappear);
        
        if (_numbofEntries == 0)
        {
            
        }
        else if (_numbofEntries == 1)
        {
            _entries[_currentGen].DeleteText();
            StartCoroutine(c_waitforAnimation(_materials[_currentGen], false));
        }
        else if (_numbofEntries == 2)
        {
            _entries[_currentGen].DeleteText();
            _entriesSeeds[_currentGen].DeleteText();

            StartCoroutine(c_waitforAnimation(_materials[_currentGen], false));
            StartCoroutine(c_waitforAnimation(_seedMaterials[_currentGen], false));
        }

        yield return new WaitForSeconds(1.5f);
        _journal.GetComponent<Animator>().ResetTrigger(Disappear);

        _journal.SetActive(false);
    }
    
    private IEnumerator c_waitforAnimation(Material mat, bool isfadingin)
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(c_InkBlotTransition(mat, isfadingin));

    }

    private IEnumerator c_InkBlotTransition(Material thisMat, bool isfadingIn)
    {
        if (isfadingIn)
        {
            while (thisMat.GetFloat(Power) < 1.05f)
            {
                float tempPower = thisMat.GetFloat(Power);
                thisMat.SetFloat(Power, tempPower += 0.1f);
                yield return new WaitForSeconds(_transitionSpeed);
            }
        }
        else
        {
            while (thisMat.GetFloat(Power) > 0f)
            {
                float tempPower = thisMat.GetFloat(Power);
                thisMat.SetFloat(Power, tempPower -= 0.1f);
                yield return new WaitForSeconds(_transitionSpeed);
            }
            
        }


    }
}
