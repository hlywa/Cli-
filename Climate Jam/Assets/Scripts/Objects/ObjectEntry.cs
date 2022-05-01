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

    public void showText()
    {
        foreach (journalText text in objectText)
        { 
            text.objectTextGO.SetActive(true);
            text.objectTextGO.GetComponent<TMP_Text>().text = text.objectText;
        }

    }

    public void DeleteText()
    {
        foreach (journalText text in objectText)
        {
            text.objectTextGO.GetComponent<TMP_Text>().text = "";
            text.objectTextGO.SetActive(false);
            
        }
    }

}
