using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanged : MonoBehaviour
{
    private void OnMouseEnter()
    {
        MouseManager.Instance.ToggleCursor(true);
    }
    private void OnMouseExit()
    {
        MouseManager.Instance.ToggleCursor(false);
    }
}
