using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UIWindow : UIComponent
{
    private int m_id;
    private UIWindowConfig m_config;
    public WindowFlagDelegate m_onSetActiveCb;

    public int Id { get { return m_id; } }
    public UIWindowConfig Config { get { return m_config; } }
    public bool IsImmortal { get { return m_config.IsImmortal; } }
    //public override UIWindowCanvas canvas { get { return null; } }

    public virtual void OnOpened(object param)
    {
    }

    public void PreInitialize(int type, UIWindowConfig config)
    {
        m_id = type;
        m_config = config;
        m_config.Initialize(this);
    }

    public void SetDisplayOrder(int openOrder)
    {
        Config.SortingOrder = CalculateSortingOrder(m_config.Priority, openOrder);
    }

    private int CalculateSortingOrder(EWindowPriority priority, int openOrder)
    {
        if (openOrder >= 100)
        {
            openOrder %= 100;
        }
        return 1 + (int)priority * 100 + openOrder;
    }

    protected override void OnSetActive(bool flag, bool isValid)
    {
        OnWindowSetActive(flag, isValid);
        if (isValid)
        {
            if (m_onSetActiveCb != null)
            {
                m_onSetActiveCb(this, flag);
            }
        }
    }

    public virtual void OnMarginalized()
    {
    }

    public virtual void OnToped()
    {
    }

    public static int Sort(UIWindow x, UIWindow y)
    {
        return x.Config.SortingOrder.CompareTo(y.Config.SortingOrder);
    }
}
