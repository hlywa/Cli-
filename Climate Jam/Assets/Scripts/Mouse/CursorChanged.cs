using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanged : MonoBehaviour
{
    
    [SerializeField] private AudioClip _objectSound;
    [SerializeField] private Transform _transform;
    public bool _doNotPlay;
    public void Start()
    {
        StartCoroutine(waitBeforeStarting());
    }

    public void Update()
    {
        if (AudioSettingsManager.Instance._sfxAudioSource.clip != _objectSound && !_doNotPlay && !JournalManager.Instance.InJournal)
        {
            StartCoroutine(waitBeforeStarting());
        }
        
        AudioSettingsManager.Instance._sfxAudioSource.volume = 1 - (Vector3.Distance(_transform.position, MouseManager.Instance._mousePos)/100) * 2;
    }

    public IEnumerator waitBeforeStarting()
    {
        yield return new WaitForSeconds(0.8f);
        AudioSettingsManager.Instance._playSFX(_objectSound,true);
    }
    
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _doNotPlay = true;
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
