using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : Singleton<MouseManager>
{
    private Vector3 mousePosition;
    [SerializeField] SpriteRenderer _cusorRenderer;
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] private Sprite _clickableCursor;
    [SerializeField] private Sprite _normalCursor;


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
