using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private GameObject _journal;
    [SerializeField] private GameObject _gen1UI;
    [SerializeField] private GameObject _gen2UI;
    [SerializeField] private GameObject _gen3UI;
    private static readonly int Power = Shader.PropertyToID("_Power");
    private Animator _thisAnimator;
    private void Start()
    {
        _thisAnimator = GetComponent<Animator>();
        
        foreach (Image childImages in _gen1UI.transform.GetComponentsInChildren<Image>())
        {
            StartCoroutine(InkBlotTransition(childImages.material, false));
        }
        foreach (Image childImages in _gen2UI.transform.GetComponentsInChildren<Image>())
        {
            StartCoroutine(InkBlotTransition(childImages.material, false));
        }
        foreach (Image childImages in _gen3UI.transform.GetComponentsInChildren<Image>())
        {
            StartCoroutine(InkBlotTransition(childImages.material, false));
        }
    }

    public void OpenJournal(int generation)
    {
        _journal.SetActive(true);
        GameObject currentGen = _gen1UI;
        switch (generation)
        {
           case 1:
               currentGen = _gen1UI;
               break;
           case 2:
               
               currentGen = _gen2UI;
               break;
           case 3:
               currentGen = _gen3UI;
               break;
        }

        foreach (Image childImages in currentGen.transform.GetComponentsInChildren<Image>())
        {
            StartCoroutine(InkBlotTransition(childImages.material, true));
        }
    }

    public IEnumerator InkBlotTransition(Material thisMat, bool isfadingIn)
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

    void Update()
    {
        
    }
}
