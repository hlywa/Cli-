using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip _mousedOverNoise;
    [SerializeField] private string _flowchartBlockName;
    private bool isClicked;
    [SerializeField] private UnityEvent _clickedEvent;


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseManager.Instance.currentFlowchart.ExecuteBlock(_flowchartBlockName);
            _clickedEvent.Invoke();
        }
    }

    private void OnMouseExit()
    {
        MouseManager.Instance.ToggleCursor(false);
        isClicked = false;
    }

    private void OnMouseEnter()
    {
        AudioSettingsManager.Instance._playSFX(_mousedOverNoise);
        MouseManager.Instance.ToggleCursor(true);
        
    }
}
