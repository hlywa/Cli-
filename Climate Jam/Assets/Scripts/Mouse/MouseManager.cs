using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class MouseManager : Singleton<MouseManager>
{
    public List<Flowchart> FlowchartsInGame;
    public Flowchart currentFlowchart;
    
    private Vector3 mousePosition;
    [SerializeField] SpriteRenderer _cusorRenderer;
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] private Sprite _clickableCursor;
    [SerializeField] private Sprite _normalCursor;

    private void Start()
    {
        currentFlowchart = FlowchartsInGame[0];
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }

    public void ToggleCursor(bool isClickable)
    {
        _cusorRenderer.sprite = isClickable ? _clickableCursor:_normalCursor;
    }
}
