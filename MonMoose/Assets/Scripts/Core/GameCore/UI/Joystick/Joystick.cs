using System;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : UIComponent
{
    [SerializeField]
    private GameObject m_innerCircle;
    [SerializeField]
    private GameObject m_outerCircle;
    [SerializeField]
    private float m_protectRadius = 5f;

    private RectTransform m_outerTrans;
    private JoystickContext m_context;
    private JoystickEvent m_event = new JoystickEvent();
    private List<TriggerInfo> m_triggerInfoList = new List<TriggerInfo>();
    private Vector2 m_downPos;
    private float m_outerRadius = 1f;

    public JoystickContext Context
    {
        get { return m_context; }
    }

    protected override void OnInit(object param)
    {
        if (m_innerCircle == null)
        {
            Debug.LogError("Error: Inner Circle Of Joystick Must Be Not Null!!!");
            return;
        }
        if (m_outerCircle == null)
        {
            Debug.LogError("Error: Outer Circle Of Joystick Must Be Not Null!!!");
            return;
        }
        if (!m_outerCircle.IsParentOf(m_innerCircle))
        {
            Debug.LogError("Error: Outer Circle Must be the Parent of Inner Circle!!!");
            return;
        }
        StructHolder<int> holder = param as StructHolder<int>;
        if (holder == null)
        {
            Debug.LogError("Error: Wrong param!!!");
            return;
        }
        m_context = JoystickManager.instance.GetContext(holder.value);
        m_context.joystick = this;
        m_event.joystick = this;
        m_event.Reset();
        m_outerTrans = m_outerCircle.transform as RectTransform;
        m_outerRadius = m_outerTrans.rect.width / 2;
        m_outerCircle.SetActiveSafely(false);
    }

    public void Cancel()
    {
        TryUp(true);
    }

    public void SetEnabled(bool isEnable)
    {
        if (!isEnable)
        {
            TryUp(true);
        }
        enabled = isEnable;
        SetActive(isEnable);
    }

    public void RegisterTrigger(GameObject trigger, int id = -1)
    {
        UIDragEventListener listener = UIDragEventListener.Get(trigger);
        listener.SetEvent(UIEventType.PointerDown, OnJoystickDown);
        listener.SetEvent(UIEventType.PointerUp, OnJoystickUp);
        listener.SetEvent(UIEventType.Drag, OnJoystickDrag);
        TriggerInfo info = GetTriggerInfo(trigger);
        info.id = id;
    }

    private TriggerInfo GetTriggerInfo(GameObject trigger)
    {
        for (int i = 0; i < m_triggerInfoList.Count; ++i)
        {
            if (m_triggerInfoList[i].triggerObj == trigger)
            {
                return m_triggerInfoList[i];
            }
        }
        TriggerInfo info = ClassPoolManager.instance.Fetch<TriggerInfo>();
        info.triggerObj = trigger;
        m_triggerInfoList.Add(info);
        return info;
    }

    private void OnJoystickDown(UIEvent e)
    {
        if (!enabled)
        {
            return;
        }
        TriggerInfo info = GetTriggerInfo(e.widget);
        m_outerCircle.SetActiveSafely(true);
        m_downPos = GetLocalPos(e.eventData.position);
        m_outerTrans.localPosition = m_downPos;

        m_event.triggerId = info.id;
        m_event.isValid = false;
        m_event.isCanceled = false;
        m_event.rate = 0f;
        m_event.offset = Vector2.zero;
        m_event.normal = Vector2.zero;
        m_event.uiEvent = e;
        m_event.state = EInputState.Down;

        HandleActionOnStateChanged();
    }

    private void OnJoystickUp(UIEvent e)
    {
        m_event.uiEvent = e;
        TryUp();
    }

    public void TryUp(bool canceled = false)
    {
        if (!enabled)
        {
            return;
        }
        if (m_event.state != EInputState.Down)
        {
            return;
        }
        m_outerCircle.SetActiveSafely(false);
        m_innerCircle.transform.localPosition = Vector2.zero;
        m_event.isCanceled = canceled;
        m_event.state = EInputState.Up;
        HandleActionOnStateChanged();
        m_event.isValid = false;
        HandleActionOnValidChanged();
        m_event.offset = Vector2.zero;
        m_event.rate = 0f;
    }

    private void OnJoystickDrag(UIEvent e)
    {
        if (!enabled)
        {
            return;
        }
        if (m_event.state != EInputState.Down)
        {
            return;
        }
        Vector2 deltaPos = GetLocalPos(e.eventData.position) - m_downPos;
        float distance = deltaPos.magnitude;
        m_event.normal = deltaPos / distance;
        if (distance > m_outerRadius)
        {
            deltaPos = m_event.normal * m_outerRadius;
            distance = m_outerRadius;
        }
        m_event.offset = deltaPos;
        m_event.rate = distance / m_outerRadius;
        m_event.uiEvent = e;
        m_innerCircle.transform.localPosition = deltaPos;
        if (distance > m_protectRadius && !m_event.isValid)
        {
            m_event.isValid = true;
            HandleActionOnValidChanged();
        }
        HandleActionOnDragUpdate();
    }

    private void HandleActionOnDragUpdate()
    {
        if (m_context != null)
        {
            HandleActionList(m_context.m_actionOnDragUpdateList);
        }
    }

    private void HandleActionOnStateChanged()
    {
        if (m_context != null)
        {
            HandleActionList(m_context.m_actionOnStateChangedList);
        }
    }

    private void HandleActionOnValidChanged()
    {
        if (m_context != null)
        {
            HandleActionList(m_context.m_actionOnValidChangedList);
        }
    }

    private void HandleActionList(List<DelegateJoystickEvent> list)
    {
        for (int i = 0; i < list.Count; ++i)
        {
            DelegateJoystickEvent action = list[i];
            if (action != null)
            {
                action(m_event);
            }
        }
    }

    private Vector2 GetLocalPos(Vector2 screenPos)
    {
        Vector2 pos = Vector2.zero;
        if (m_outerCircle != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_outerCircle.transform.parent as RectTransform, screenPos, canvas.uiCamera.Camera, out pos);
        }
        return pos;
    }

    protected override void OnUninit()
    {
        for (int i = 0; i < m_triggerInfoList.Count; ++i)
        {
            m_triggerInfoList[i].Release();
        }
        m_context.joystick = null;
        m_context = null;
    }

    private class TriggerInfo : ClassPoolObj
    {
        public GameObject triggerObj;
        public int id;

        public override void OnRelease()
        {
            id = -1;
            triggerObj = null;
        }
    }
}