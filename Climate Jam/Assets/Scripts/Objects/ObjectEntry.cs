using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Febucci.UI;
using TMPro;
using UnityEngine;

public class ObjectEntry : MonoBehaviour
{
    [System.Serializable]
    public class journalText
    {
        public GameObject objectTextGO;
        public string objectText;
    }
    
    [SerializeField]private List<journalText> objectText;
    [SerializeField] private Material _fadeMat;
    public void showText()
    {
        foreach (journalText text in objectText)
        { 
            text.objectTextGO.SetActive(true);
            if (text.objectTextGO.GetComponent<TMP_Text>())
            {
                text.objectTextGO.GetComponent<TMP_Text>().text = text.objectText;
            }
        }

    }

    public void DeleteText()
    {
        if (_fadeMat != null)
        {
            StartCoroutine(JournalManager.Instance.c_InkBlotTransition(_fadeMat, false));
        }

        foreach (journalText text in objectText)
        {
            if (text.objectTextGO.GetComponent<TMP_Text>())
            {
                text.objectTextGO.GetComponent<TMP_Text>().text = "";
            }
            text.objectTextGO.SetActive(false);
            
        }
    }

}
