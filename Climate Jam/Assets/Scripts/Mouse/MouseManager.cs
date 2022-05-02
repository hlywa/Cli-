using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class MouseManager : Singleton<MouseManager>
{
    public List<Flowchart> FlowchartsInGame;
    public Flowchart currentFlowchart;
    public Vector3 _mousePos;
    private Vector3 mousePosition;
    [SerializeField] SpriteRenderer _cusorRenderer;
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] private Sprite _clickableCursor;
    [SerializeField] private Sprite _normalCursor;
    public AudioClip _mouseClickSound;
    private void Start()
    {
        mousePosition = Input.mousePosition;
        _mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
        currentFlowchart = FlowchartsInGame[0];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioSettingsManager.Instance._playSFX(_mouseClickSound, false);
        }
        
        
        mousePosition = Input.mousePosition;
        _mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, _mousePos, moveSpeed);
    }

    public void ToggleCursor(bool isClickable)
    {
        _cusorRenderer.sprite = isClickable ? _clickableCursor:_normalCursor;
    }
}
