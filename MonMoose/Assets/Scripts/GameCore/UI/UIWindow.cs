using System;
using UnityEngine;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine.EventSystems;

public class UIWindow : UIComponent
{
    private int m_id;
    private UIWindowConfig m_config;
    private List<UIComponent> m_componentList = new List<UIComponent>();
    private List<IUIUpdatable> m_updatableList = new List<IUIUpdatable>();
    private List<UIComponent> m_initComponentList = new List<UIComponent>();
    private UIWindowProcess m_appearProcess;
    private UIWindowProcess m_hideProcess;
    public WindowFlagDelegate m_onSetActiveCb;

    public int Id { get { return m_id; } }
    public UIWindowConfig Config { get { return m_config; } }
    public bool IsImmortal { get { return m_config.IsImmortal; } }
    public override UIWindowCanvas canvas { get { return null; } }

    public void Initialize(int type, UIWindowConfig config)
    {
        m_id = type;
        m_config = config;
        Initialize(this);
    }

    protected override void OnInit(object param)
    {
        m_config.Initialize(this);
        UIComponent[] components = gameObject.GetComponentsInChildren<UIComponent>(true);
        for (int i = 0; i < components.Length; ++i)
        {
            if (components[i].needAutoInit)
            {
                components[i].Initialize(this);
            }
        }
    }

    public void AddComponent(UIComponent component)
    {
        if (component != this)
        {
            m_componentList.Add(component);
        }
        m_initComponentList.Add(component);
        IUIUpdatable updatable = component as IUIUpdatable;
        if (updatable != null)
        {
            m_updatableList.Add(updatable);
        }
    }

    public void RemoveComponent(UIComponent component)
    {
        m_componentList.Remove(component);
        IUIUpdatable updatable = component as IUIUpdatable;
        if (updatable != null)
        {
            m_updatableList.Remove(updatable);
        }
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

    public void UpdateAlways(float deltaTime)
    {
        for (int i = 0; i < m_initComponentList.Count; ++i)
        {
            m_initComponentList[i].LateInit();
        }
        m_initComponentList.Clear();
        for (int i = 0; i < m_updatableList.Count; ++i)
        {
            m_updatableList[i].UpdateFloat(deltaTime);
        }
    }

    protected override void OnSetActive(bool flag, bool isValid)
    {
        for (int i = 0; i < m_componentList.Count; ++i)
        {
            m_componentList[i].OnWindowSetActive(flag, isValid);
        }
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

    public void Destroy()
    {
        for (int i = 0; i < m_componentList.Count; ++i)
        {
            m_componentList[i].UnInit();
        }
        UnInit();
        m_componentList.Clear();
        m_updatableList.Clear();
        Destroy(gameObject);
    }

    public static int Sort(UIWindow x, UIWindow y)
    {
        return x.Config.SortingOrder.CompareTo(y.Config.SortingOrder);
    }
}
