using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip _mousedOverNoise;
    private bool isClicked;
    [SerializeField] private UnityEvent _clickedEvent;


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
