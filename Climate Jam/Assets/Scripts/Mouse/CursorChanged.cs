using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanged : MonoBehaviour
{
    
    [SerializeField] private AudioClip _objectSound;
    [SerializeField] private Transform _transform;
    public void Start()
    {
        AudioSettingsManager.Instance._playSFX(_objectSound,true);
    }

    public void Update()
    {
        AudioSettingsManager.Instance._sfxAudioSource.volume = 1 - (Vector3.Distance(_transform.position, MouseManager.Instance._mousePos)/100) * 2;
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioSettingsManager.Instance._stopSFX();
        }
    }

    private void OnMouseEnter()
    {
        MouseManager.Instance.ToggleCursor(true);
        
    }
    private void OnMouseExit()
    {
        MouseManager.Instance.ToggleCursor(false);
    }
}
