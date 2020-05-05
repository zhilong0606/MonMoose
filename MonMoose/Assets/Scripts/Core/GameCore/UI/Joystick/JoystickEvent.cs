using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DelegateJoystickEvent(JoystickEvent e);

public class JoystickEvent
{
    public Joystick joystick;
    public int triggerId;

    public bool isValid;
    public bool isCanceled;

    public float rate;
    public Vector2 normal;
    public Vector2 offset;
    public UIEvent uiEvent;
    public EInputState state = EInputState.Up;

    public void Reset()
    {
        triggerId = -1;

        isValid = false;
        isCanceled = false;

        rate = 0f;
        normal = Vector2.zero;
        offset = Vector2.zero;
        uiEvent = null;
        state = EInputState.Up;
    }
}
