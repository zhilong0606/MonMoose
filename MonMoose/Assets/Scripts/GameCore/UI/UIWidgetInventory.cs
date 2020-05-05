using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWidgetInventory : GameObjectInventory, IUIWindowHolder
{
    [SerializeField]
    private int m_id = -1;

    private UIWindow m_window;

    public int Id { get { return m_id; } }
    public UIWindow window { get { return m_window; } set { m_window = value; } }

    public T GetComponent<T>(int index, bool needInit, object param = null) where T : UIComponent
    {
        GameObject go = Get(index);
        if (go == null)
        {
            return null;
        }
        T component = go.GetComponent<T>();
        if (component != null && needInit)
        {
            component.Initialize(m_window, param);
        }
        return component;
    }

    public T AddComponent<T>(int index, bool needInit, object param = null) where T : UIComponent
    {
        GameObject go = Get(index);
        if (go == null)
        {
            return null;
        }
        T component = go.AddComponent<T>();
        if (component != null && needInit)
        {
            component.Initialize(m_window, param);
        }
        return component;
    }
}
