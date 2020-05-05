using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

public class JoystickManager : Singleton<JoystickManager>
{
    private Dictionary<int, JoystickContext> m_joystickContextMap = new Dictionary<int, JoystickContext>();

    public JoystickContext CreateContext(int id)
    {
        JoystickContext context = new JoystickContext();
        context.joystickId = id;
        m_joystickContextMap.Add(id, context);
        return context;
    }

    public JoystickContext GetContext(int id, bool force = false)
    {
        JoystickContext context;
        if (!m_joystickContextMap.TryGetValue(id, out context))
        {
            if (force)
            {
                context = CreateContext(id);
            }
        }
        return context;
    }

    public Joystick GetJoystick(int id)
    {
        JoystickContext context = GetContext(id);
        if (context != null)
        {
            return context.joystick;
        }
        return null;
    }

    public void RegisterActionStateChanged(int id, DelegateJoystickEvent action)
    {
        JoystickContext context = GetContext(id, true);
        context.m_actionOnStateChangedList.Add(action);
    }

    public void UnregisterActionStateChanged(int id, DelegateJoystickEvent action)
    {
        JoystickContext context = GetContext(id);
        if (context != null)
        {
            context.m_actionOnStateChangedList.Remove(action);
        }
    }

    public void RegisterActionValidChanged(int id, DelegateJoystickEvent action)
    {
        JoystickContext context = GetContext(id, true);
        context.m_actionOnValidChangedList.Add(action);
    }

    public void UnregisterActionValidChanged(int id, DelegateJoystickEvent action)
    {
        JoystickContext context = GetContext(id);
        if (context != null)
        {
            context.m_actionOnValidChangedList.Remove(action);
        }
    }

    public void RegisterActionDragUpdate(int id, DelegateJoystickEvent action)
    {
        JoystickContext context = GetContext(id, true);
        context.m_actionOnDragUpdateList.Add(action);
    }

    public void UnregisterActionDragUpdate(int id, DelegateJoystickEvent action)
    {
        JoystickContext context = GetContext(id);
        if (context != null)
        {
            context.m_actionOnDragUpdateList.Remove(action);
        }
    }
}
