using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ObjectEntry : MonoBehaviour
{
    [SerializeField]private List<GameObject> objectText;

    public void Start()
    {
        DeleteText();
    }

    public void showText()
    {
        foreach (GameObject text in objectText)
        {
            text.SetActive(true);
        }
    }

    public void DeleteText()
    {
        foreach (GameObject text in objectText)
        {
            text.SetActive(false);
        }
    }

}
