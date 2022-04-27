using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
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
        
        _journal.SetActive(false);
    }

    public void Update()
    {
        _currentGen = GenerationManager.Instance.ReturnGeneration();
    }

    public void OpenJournal(int numberofEntries)
    {
        _journal.SetActive(true);
        _currentGen = GenerationManager.Instance.ReturnGeneration();
        GameObject tempObj = genObjects[_currentGen];
        GameObject tempseed = genObjSeed[_currentGen];
        
        _numbofEntries = numberofEntries;
        
        if (numberofEntries == 1)
        {
            foreach (Image childImages in tempObj.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(childImages.material, true));
            }
        }

        if (numberofEntries == 2)
        {
            foreach (Image childImages in tempObj.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(childImages.material, true));
            }
            
            foreach (Image tempseeds in tempseed.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(tempseeds.material, true));
            }
        }

        GenerationManager.Instance.toggleBtnsOn(true);
    }

    public void CloseJournal()
    {
        StartCoroutine(c_closeJournal());
    }

    private IEnumerator c_closeJournal()
    {
        GameObject tempObj = genObjects[_currentGen];
        GameObject tempseed = genObjSeed[_currentGen];
        
        if (_numbofEntries == 1)
        {
            foreach (Image childImages in tempObj.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(childImages.material, false));
            }
        }
        if (_numbofEntries == 2)
        {
            
            foreach (Image childImages in tempObj.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(childImages.material, true));
            }
            
            foreach (Image tempseeds in tempseed.transform.GetComponentsInChildren<Image>())
            {
                StartCoroutine(c_waitforAnimation(tempseeds.material, false));
            }
        }
        yield return new WaitForSeconds(0.5f);
        _journal.GetComponent<Animator>().SetTrigger(Disappear);
        yield return new WaitForSeconds(1.5f);
        GenerationManager.Instance.toggleBtnsOn(false);
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
