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
    [SerializeField] private List<GameObject> genObjects;
    [SerializeField] private List<GameObject> genObjSeed;
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

        foreach (GameObject gmObj in genObjects)
        {
            foreach (Image childImages in gmObj.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_InkBlotTransition(childImages.material, false));
            }
        }
        foreach (GameObject gmObj in genObjSeed)
        {
            foreach (Image childImages in gmObj.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_InkBlotTransition(childImages.material, false));
            }
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
    public void OpenJournal()
    {
        _journal.SetActive(true);
        _currentGen = GenerationManager.Instance.ReturnGeneration();
        GameObject tempObj = genObjects[_currentGen];
        GameObject tempseed = genObjSeed[_currentGen];
        
        if (_numbofEntries == 1)
        {
            foreach (Image childImages in tempObj.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(childImages.material, true));
                
                childImages.GetComponent<ObjectEntry>().showText();
            }
        }

        if (_numbofEntries == 2)
        {
            foreach (Image childImages in tempObj.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(childImages.material, true));
                childImages.GetComponent<ObjectEntry>().showText();
            }
            
            foreach (Image tempseeds in tempseed.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(tempseeds.material, true));
                tempseeds.GetComponent<ObjectEntry>().showText();
            }
        }

    }

    public void CloseJournal()
    {
        StartCoroutine(c_closeJournal());
    }

    public void NextGeneration()
    {
        GameObject tempObj = genObjects[_currentGen];
        GameObject tempseed = genObjSeed[_currentGen];
 
            foreach (Image childImages in tempObj.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(childImages.material, true));
                childImages.GetComponent<ObjectEntry>().DeleteText();

            }
            
            foreach (Image tempseeds in tempseed.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(tempseeds.material, false));
                tempseeds.GetComponent<ObjectEntry>().DeleteText();

            }

        switch (_currentGen)
        {
            case 0:
                break;
            case 1:
                _journal.GetComponent<Animator>().SetTrigger("g1g2");
                _journal.GetComponent<SpriteRenderer>().sprite = _g2;
                break;
                case 2:
                    _journal.GetComponent<Animator>().SetTrigger("g2g3"); 
                    _journal.GetComponent<SpriteRenderer>().sprite = _g3;
                    break;

        }
    }
    private IEnumerator c_closeJournal()
    {
        GameObject tempObj = genObjects[_currentGen];
        GameObject tempseed = genObjSeed[_currentGen];
        yield return new WaitForSeconds(0.5f);
        _journal.GetComponent<Animator>().SetTrigger(Disappear);
        
        if (_numbofEntries == 1)
        {
            foreach (Image childImages in tempObj.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(childImages.material, false));
                childImages.GetComponent<ObjectEntry>().DeleteText();

            }
        }
        if (_numbofEntries == 2)
        {
            
            foreach (Image childImages in tempObj.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(childImages.material, true));
                childImages.GetComponent<ObjectEntry>().DeleteText();

            }
            
            foreach (Image tempseeds in tempseed.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(tempseeds.material, false));
                tempseeds.GetComponent<ObjectEntry>().DeleteText();

            }
        }
        yield return new WaitForSeconds(1.5f);
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
