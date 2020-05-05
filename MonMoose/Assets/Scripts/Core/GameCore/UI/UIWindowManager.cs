using UnityEngine;
using System.Collections.Generic;
using MonMoose.Core;

public class UIWindowManager : Singleton<UIWindowManager>
{
    private GameObject m_uiRoot;
    private GameObjectPool m_cameraPool;
    private Dictionary<int, UIWindow> m_windowMap = new Dictionary<int, UIWindow>();
    private Dictionary<int, UIWindowContext> m_windowContextMap = new Dictionary<int, UIWindowContext>();
    private List<UIWindow> m_windowList = new List<UIWindow>();
    private List<UIWindow> m_sortList = new List<UIWindow>();
    private List<UICamera> m_cameraList = new List<UICamera>();
    private int m_cameraIndex = 0;
    private List<int> m_releaseList = new List<int>();
    private Stack<UIWindow> m_windowStack = new Stack<UIWindow>();
    private UIWindow m_topWindow = null;
    private bool m_needUpdateSort = false;
    private int m_cameraStartDepth = 10;

    protected override void Init()
    {
        GameObject immortalRoot = GameObject.Find("Immortal");
        m_uiRoot = immortalRoot.FindChild("UIRoot");
        m_cameraPool = immortalRoot.FindChild("UICameraRoot").GetComponent<GameObjectPool>();
        m_cameraPool.Init(OnCameraInit);
        TickManager.instance.RegisterGlobalTick(Tick);
    }

    private void OnCameraInit(GameObjectPool.PoolObjHolder holder)
    {
        UICamera camera = holder.GetComponent<UICamera>();
        camera.Init();
    }

    public void RegisterWindowContext(int windowId, UIWindowContext context)
    {
        m_windowContextMap.Add(windowId, context);
    }

    public UIWindowContext GetWindowContext(int windowId)
    {
        UIWindowContext context;
        if (m_windowContextMap.TryGetValue(windowId, out context))
        {
            return context;
        }
        return null;
    }

    public UIWindow OpenWindow(int windowId)
    {
        UIWindow window = null;
        if (!m_windowMap.TryGetValue(windowId, out window))
        {
            window = CreateWindow(windowId);
        }
        else
        {
            window.SetActive(true);
        }
        return window;
    }

    public T OpenWindow<T>(int windowId) where T : UIWindow
    {
        return OpenWindow(windowId) as T;
    }

    public T CreateWindow<T>(int windowId) where T : UIWindow
    {
        return CreateWindow(windowId) as T;
    }

    public UIWindow CreateWindow(int windowId)
    {
        UIWindow window = null;
        if (m_windowMap.TryGetValue(windowId, out window))
        {
            Debug.LogError(string.Format("Same Window has Already Created: {0}", windowId));
        }
        else
        {
            UIWindowContext context = GetWindowContext(windowId);
            if (context != null)
            {
                GameObject prefab = ResourceManager.instance.GetPrefab(context.Path);
                if (prefab != null)
                {
                    GameObject go = Object.Instantiate(prefab, m_uiRoot.transform);
                    UIWindowConfig config = go.GetComponent<UIWindowConfig>();
                    window = go.AddComponent(context.Type) as UIWindow;
                    if (window != null)
                    {
                        window.Initialize(windowId, config);
                        if (window.gameObject != null && window.isInitialized)
                        {
                            window.m_onSetActiveCb = OnWindowSetActive;
                            AddWindow(windowId, window);
                            NeedUpdateSort();
                        }
                    }
                    else
                    {
                        Debug.LogError(string.Format("{0} is Not UIWindow", windowId));
                    }
                }
                else
                {
                    Debug.LogError(string.Format("{0} is Not Correct Path of {1}", context.Path, windowId));
                }
            }
            else
            {
                Debug.LogError(string.Format("{0} Don't Have Context", windowId));
            }
        }
        if (window != null && window.isActiveSelf && window.Config.NeedStack)
        {
            SetTop(window);
        }
        return window;
    }

    private void OnWindowSetActive(UIWindow window, bool flag)
    {
        if (window.Config.NeedStack && flag)
        {
            SetTop(window);
        }
        if (window.Config.ActiveToTop && flag)
        {
            m_windowList.Remove(window);
            m_windowList.Add(window);
        }
        NeedUpdateSort();
    }

    private void NeedUpdateSort()
    {
        m_needUpdateSort = true;
    }

    public void UpdateWindowSort()
    {
        for (int i = 0; i < m_windowList.Count; ++i)
        {
            UIWindow window = m_windowList[i];
            if (window != null && window.isActiveInHierarchy)
            {
                window.SetDisplayOrder(i);
            }
        }
        m_sortList.AddRange(m_windowList);
        m_sortList.Sort(UIWindow.Sort);
        m_cameraIndex = 0;
        int depth = m_cameraStartDepth;
        UICamera lastCamera = null;
        for (int i = 0; i < m_sortList.Count; ++i)
        {
            UIWindow window = m_sortList[i];
            if (window.isActiveInHierarchy)
            {
                window.Config.UpdateCameraAndDepth(ref lastCamera, ref depth);
            }
        }
        for (int i = m_cameraList.Count - 1; i >= m_cameraIndex; --i)
        {
            m_cameraPool.Release(m_cameraList[i].gameObject);
            m_cameraList.RemoveAt(i);
        }
        m_sortList.Clear();
    }

    public UICamera NewCamera()
    {
        UICamera camera = null;
        if (m_cameraIndex < m_cameraList.Count)
        {
            camera = m_cameraList[m_cameraIndex];
        }
        else
        {
            camera = m_cameraPool.FetchComponent<UICamera>();
            m_cameraList.Add(camera);
        }
        m_cameraIndex++;
        return camera;
    }

    public void PopToBaseWindow()
    {
        if (m_windowStack.Count > 0)
        {
            if (m_topWindow != null)
            {
                m_topWindow.OnMarginalized();
                m_topWindow.SetActive(false);
            }
            while (m_windowStack.Count > 0)
            {
                m_topWindow = m_windowStack.Pop();
            }
            if (m_topWindow != null)
            {
                m_topWindow.SetActive(true);
                m_topWindow.OnToped();
            }
        }
    }

    public void Pop()
    {
        if (m_windowStack.Count > 0)
        {
            if (m_topWindow != null)
            {
                m_topWindow.OnMarginalized();
                m_topWindow.SetActive(false);
            }
            m_topWindow = m_windowStack.Pop();
            m_topWindow.SetActive(true);
            m_topWindow.OnToped();
        }
    }

    public void SetTop(UIWindow window)
    {
        if (m_topWindow == window)
        {
            return;
        }
        if (m_topWindow != null)
        {
            m_topWindow.OnMarginalized();
            m_topWindow.SetActive(false);
            m_windowStack.Push(m_topWindow);
        }
        m_topWindow = window;
        m_topWindow.SetActive(true);
        m_topWindow.OnToped();
    }

    public bool IsTopBaseWindow()
    {
        return m_windowStack.Count == 0;
    }

    public UIWindow GetTop()
    {
        return m_topWindow;
    }

    public void ClearStack()
    {
        m_windowStack.Clear();
        m_topWindow = null;
    }

    public UIWindow GetWindow(int windowId, bool bForce = false)
    {
        UIWindow window = null;
        if (!m_windowMap.TryGetValue(windowId, out window) && bForce)
        {
            window = CreateWindow(windowId);
        }
        return window;
    }

    public T GetWindow<T>(int windowId, bool bForce = false) where T : UIWindow
    {
        return GetWindow(windowId, bForce) as T;
    }

    public void AddWindow(int windowId, UIWindow window)
    {
        if (!m_windowMap.ContainsKey(windowId))
        {
            m_windowMap.Add(windowId, window);
            m_windowList.Add(window);
        }
    }

    public void RemoveWindow(int windowId)
    {
        m_windowMap.Remove(windowId);
        for (int i = 0; i < m_windowList.Count; ++i)
        {
            if (windowId == m_windowList[i].Id)
            {
                m_windowList.Remove(m_windowList[i]);
            }
        }
        NeedUpdateSort();
    }

    public void Tick(float deltaTime)
    {
        if (m_needUpdateSort)
        {
            UpdateWindowSort();
            m_needUpdateSort = false;
        }
        Dictionary<int, UIWindow>.Enumerator enumerator = m_windowMap.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.UpdateAlways(deltaTime);
        }
        enumerator.Dispose();
    }

    public void DestroyWindow(int windowId)
    {
        UIWindow window;
        if (m_windowMap.TryGetValue(windowId, out window))
        {
            RemoveWindow(windowId);
            window.Destroy();
        }
    }

    public void DestroyWindow(UIWindow window)
    {
        DestroyWindow(window.Id);
    }

    public void DestroyAllWindow(int[] excepts = null)
    {
        Dictionary<int, UIWindow>.Enumerator enumerator = m_windowMap.GetEnumerator();
        while (enumerator.MoveNext())
        {
            UIWindow window = enumerator.Current.Value;
            if (!window.IsImmortal)
            {
                bool needClose = true;
                if (excepts != null)
                {
                    for (int i = 0; i < excepts.Length; ++i)
                    {
                        if (excepts[i] == enumerator.Current.Key)
                        {
                            needClose = false;
                            break;
                        }
                    }
                }
                if (needClose)
                {
                    m_releaseList.Add(window.Id);
                }
            }
        }
        enumerator.Dispose();
        for (int i = 0; i < m_releaseList.Count; ++i)
        {
            DestroyWindow(m_releaseList[i]);
        }
        m_releaseList.Clear();
    }
}