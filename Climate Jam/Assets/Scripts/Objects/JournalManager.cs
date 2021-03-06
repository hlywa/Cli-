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
    [SerializeField] private List<ObjectEntry> _entriesTrash;
    
    [SerializeField] private List<Material> _materials;
    [SerializeField] private List<Material> _seedMaterials;
    
    [SerializeField] private AudioClip _openJournal;
    [SerializeField] private AudioClip _closeJournal;
    [SerializeField] private AudioClip _writingInJournal;

    private static readonly int Power = Shader.PropertyToID("_Power");
    private Animator _thisAnimator;
    private static readonly int Disappear = Animator.StringToHash("Disappear");
    private int _currentGen;
    private int _numbofEntries;
    public bool InJournal;
    
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
        AudioSettingsManager.Instance._playSFX(_openJournal, false);

        yield return new WaitForSeconds(1.4f);
        
        AudioSettingsManager.Instance._playSFX(_writingInJournal, false);
        
        if (_currentGen == 1)
        {
            _journal.GetComponent<Animator>().SetTrigger("g1g2");
            yield return new WaitForSeconds(1.4f);

        }
        else if (_currentGen == 2)
        {
            _journal.GetComponent<Animator>().SetTrigger("g2g3"); 
            yield return new WaitForSeconds(1.4f);

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

            StartCoroutine(c_waitforAnimation(_materials[_currentGen], true));
            if (GenerationManager.Instance._didPlayerSucceed[_currentGen])
            {
                _entriesSeeds[_currentGen].showText();
                StartCoroutine(c_waitforAnimation(_seedMaterials[_currentGen], true));
            }
            else
            {
                _entriesTrash[_currentGen].showText();
                
            }

        }

    }
    
    public void OpenJournal()
    {
        _journal.SetActive(true);
        InJournal = true;
        _currentGen = GenerationManager.Instance.ReturnGeneration();
        StartCoroutine(c_openJournal());
    }

    public void CloseJournal()
    {
        InJournal = false;
        StartCoroutine(c_closeJournal());
    }

    public void NextGeneration(int _currentGene)
    {
        StartCoroutine(c_nextGen(_currentGene));
    }
    
    private IEnumerator c_nextGen(int _currentGene)
    {
        yield return new WaitForSeconds(6f);
        
        
        StartCoroutine(c_waitforAnimation(_materials[_currentGene - 1], false));
        _entries[_currentGene - 1].DeleteText();
        
        if (GenerationManager.Instance._didPlayerSucceed[_currentGene - 1])
        {
            _entriesSeeds[_currentGene - 1].DeleteText();
            StartCoroutine(c_waitforAnimation(_seedMaterials[_currentGene - 1], false));
        }
        else
        {
            _entriesTrash[_currentGene - 1].DeleteText();
                
        }
        
        _journal.GetComponent<Animator>().SetTrigger(Disappear);

        yield return new WaitForSeconds(1.3F);
        changeEntryNumber(0);
        _journal.SetActive(false);
        MouseManager.Instance.currentFlowchart.ExecuteBlock("First");
    }
    private IEnumerator c_closeJournal()
    {
        AudioSettingsManager.Instance._playSFX(_closeJournal, false);

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
            StartCoroutine(c_waitforAnimation(_materials[_currentGen], false));
            
            if (GenerationManager.Instance._didPlayerSucceed[_currentGen])
            {
                _entriesSeeds[_currentGen].DeleteText();
                StartCoroutine(c_waitforAnimation(_seedMaterials[_currentGen], false));
            }
            else
            {
                _entriesTrash[_currentGen].DeleteText();
                
            }
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
