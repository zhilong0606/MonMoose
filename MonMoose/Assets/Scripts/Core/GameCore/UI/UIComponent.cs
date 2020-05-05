using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine.EventSystems;

public class UIComponent : UIBehaviour, IUIWindowHolder
{
    private bool m_isInitialized;
    private UIWindow m_window;
    private UIWindowCanvas m_canvas;
    private Dictionary<int, IEnumerator> m_awakeMap = new Dictionary<int, IEnumerator>();
    private List<IEnumerator> m_awakeList = new List<IEnumerator>();
    private UIWidgetInventory m_widgetInventory;

    public UIWindow window { get { return m_window; } }
    public bool isInitialized { get { return m_isInitialized; } }
    public virtual bool needAutoInit { get { return false; } }
    public virtual int widgetInventoryId { get { return -1; } }
    public bool isActiveSelf { get { return gameObject != null && gameObject.activeSelf; } }
    public bool isActiveInHierarchy { get { return gameObject != null && gameObject.activeInHierarchy; } }
    public virtual UIWindowCanvas canvas { get { if (m_canvas == null) m_canvas = FindCanvas(); return m_canvas; } }
    public UIWidgetInventory widgetInventory { get { return m_widgetInventory; } }

    public void Initialize(UIWindow window, object param = null)
    {
        if (m_isInitialized)
        {
            Debug.LogWarning("Warn: Initialize Multi Time!!!   " + GetType());
            return;
        }
        m_window = window;
        m_widgetInventory = GetInventory(widgetInventoryId);
        try
        {
            OnInit(param);
            m_isInitialized = true;
        }
        catch
        {
            Debug.LogError("Error: Initialize Failed!!!   " + GetType());
            Destroy(gameObject);
        }
        if (m_isInitialized)
        {
            window.AddComponent(this);
            RegisterListener();
        }
    }

    public void Initialize<T>(UIWindow window, T param) where T : struct
    {
        StructHolder<T> holder = ClassPoolManager.instance.Fetch<StructHolder<T>>();
        holder.value = param;
        Initialize(window, (object)holder);
        holder.Release();
    }

    public void UnInit()
    {
        if (m_isInitialized)
        {
            OnUninit();
            UnregisterListener();
            m_awakeMap.Clear();
            m_awakeList.Clear();
            m_window = null;
            m_isInitialized = false;
        }
    }

    protected override void OnEnable()
    {
        for (int i = 0; i < m_awakeList.Count; ++i)
        {
            HandleAwakeIter(m_awakeList[i]);
        }
        m_awakeList.Clear();
        m_awakeMap.Clear();
    }

    public void TryUpdateWidget(int id, IEnumerator iter)
    {
        if (isActiveAndEnabled)
        {
            HandleAwakeIter(iter);
        }
        else
        {
            if (m_awakeMap.ContainsKey(id))
            {
                m_awakeList.Remove(m_awakeMap[id]);
                m_awakeMap[id] = iter;
            }
            else
            {
                m_awakeMap.Add(id, iter);
            }
            m_awakeList.Add(iter);
        }
    }

    private void HandleAwakeIter(IEnumerator iter)
    {
        while (iter.MoveNext())
        {
        }
    }

    public void SetActive(bool flag)
    {
        bool isValid = flag != isActiveSelf;
        if (flag)
        {
            if (isValid)
            {
                gameObject.SetActiveSafely(true);
            }
            OnSetActive(true, isValid);
        }
        else
        {
            OnSetActive(false, isValid);
            if (isValid)
            {
                gameObject.SetActiveSafely(false);
            }
        }
    }

    private UIWindowCanvas FindCanvas()
    {
        Transform t = transform;
        List<Component> l = ListPool<Component>.Get();
        UIWindowCanvas canvas = null;
        while (t != null)
        {
            bool needBreak = false;
            l.Clear();
            t.GetComponents(l);
            for (int i = 0; i < l.Count; ++i)
            {
                Component compo = l[i];
                if (compo is UIWindowCanvas)
                {
                    canvas = compo as UIWindowCanvas;
                    needBreak = true;
                }
                else if (compo is UIWindow)
                {
                    needBreak = true;
                }
            }
            if (needBreak)
            {
                break;
            }
            t = t.parent;
        }
        ListPool<Component>.Release(l);
        return canvas;
    }

    public UIWidgetInventory GetInventory(int id)
    {
        UIWidgetInventory result = null;
        List<Component> l = ListPool<Component>.Get();
        transform.GetComponents(l);
        for (int i = 0; i < l.Count; ++i)
        {
            UIWidgetInventory inventory = l[i] as UIWidgetInventory;
            if (inventory != null && inventory.Id == id)
            {
                result = inventory;
                break;
            }
        }
        ListPool<Component>.Release(l);
        return result;
    }
    public T GetComponentToWidget<T>(int index, bool needInit, object param = null) where T : UIComponent
    {
        if (widgetInventory != null)
        {
            return widgetInventory.GetComponent<T>(index, needInit, param);
        }
        return null;
    }

    public T AddComponentToWidget<T>(int index, bool needInit, object param = null) where T : UIComponent
    {
        if (widgetInventory != null)
        {
            return widgetInventory.AddComponent<T>(index, needInit, param);
        }
        return null;
    }

    public T GetComponentToWidget<T>(int index) where T : Component
    {
        if (widgetInventory != null)
        {
            return widgetInventory.GetComponent<T>(index);
        }
        return null;
    }

    public T AddComponentToWidget<T>(int index) where T : Component
    {
        if (widgetInventory != null)
        {
            return widgetInventory.AddComponent<T>(index);
        }
        return null;
    }

    public GameObject GetWidget(int index)
    {
        if (widgetInventory != null)
        {
            return widgetInventory.Get(index);
        }
        return null;
    }

    protected void DebugLog(string str)
    {
        Debug.Log(string.Format("UILog: <color=#4BDAFF>{0}</color", str));
    }

    protected virtual void OnInit(object param) { }
    protected virtual void OnUninit() { }
    protected virtual void RegisterListener() { }
    protected virtual void UnregisterListener() { }
    protected virtual void OnSetActive(bool flag, bool isValid) { }
    protected virtual void Update() { }
    public virtual void LateInit() { }
    public virtual void OnWindowSetActive(bool flag, bool isValid) { }
}
