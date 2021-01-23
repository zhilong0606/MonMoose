using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace MonMoose.Core
{
    public class UIComponent : UIBehaviour
    {
        private static List<Component> m_catchedComponentList = new List<Component>();

        private bool m_isInitialized;
        private UIWindowCanvas m_canvas;
        private UIComponent m_parent;
        private Dictionary<int, IEnumerator> m_awakeMap = new Dictionary<int, IEnumerator>();
        private List<IEnumerator> m_awakeList = new List<IEnumerator>();
        private Dictionary<int, UIWidgetInventory> m_widgetInventoryMap = new Dictionary<int, UIWidgetInventory>();
        private List<UIComponent> m_componentList = new List<UIComponent>();
        private UIWidgetInventory m_cachedInventory;
        private bool m_isDestroying;

        public bool isInitialized
        {
            get { return m_isInitialized; }
        }

        public virtual bool needAutoInit
        {
            get { return false; }
        }

        public bool isActiveSelf
        {
            get { return gameObject != null && gameObject.activeSelf; }
        }

        public bool isActiveInHierarchy
        {
            get { return gameObject != null && gameObject.activeInHierarchy; }
        }

        public virtual UIWindowCanvas canvas
        {
            get
            {
                if (m_canvas == null) m_canvas = FindCanvas();
                return m_canvas;
            }
        }

        public bool isDestroying
        {
            get { return m_isDestroying; }
        }

        public void Initialize(UIComponent parent, object param = null)
        {
            if (m_isInitialized)
            {
                Debug.LogWarning("Warn: Initialize Multi Time!!!   " + GetType());
                return;
            }
            CollectWidgetInventory();
#if USE_TRYCATCH
        try
#endif
            {
                OnInit(param);
                m_isInitialized = true;
            }
#if USE_TRYCATCH
            catch (Exception e)
            {
                Debug.LogError("Error: Initialize Failed!!!   " + e);
                Destroy(gameObject);
            }
#endif
            if (m_isInitialized)
            {
                BindParent(parent);
                RegisterListener();
            }
        }

        public void UnInit()
        {
            if (m_isInitialized)
            {
                OnUnInit();
                UnRegisterListener();
                for (int i = m_componentList.Count - 1; i >= 0; --i)
                {
                    m_componentList[i].UnInit();
                }
                m_componentList.Clear();
                m_awakeMap.Clear();
                m_awakeList.Clear();
                BindParent(null);
                m_isInitialized = false;
            }
        }

        public void Destroy()
        {
            m_isDestroying = true;
            UnInit();
            Destroy(gameObject);
        }

        public void BindParent(UIComponent parent)
        {
            if (parent == m_parent)
            {
                return;
            }
            if (m_parent != null)
            {
                m_parent.RemoveComponent(this);
            }
            m_parent = parent;
            if (m_parent != null)
            {
                m_parent.AddComponent(this);
            }
            m_canvas = null;
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

        public void AddComponent(UIComponent component)
        {
            if (component != this)
            {
                m_componentList.Add(component);
            }
        }

        public void RemoveComponent(UIComponent component)
        {
            m_componentList.Remove(component);
        }

        public void UpdateAlways(float deltaTime)
        {
            IUIUpdatable updatable = this as IUIUpdatable;
            if (updatable != null)
            {
                updatable.UpdateFloat(deltaTime);
            }
            for (int i = 0; i < m_componentList.Count; ++i)
            {
                m_componentList[i].UpdateAlways(deltaTime);
            }
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
            if (m_canvas == null)
            {
                Transform t = transform;
                while (t != null)
                {
                    bool needBreak = false;
                    m_catchedComponentList.Clear();
                    t.GetComponents(m_catchedComponentList);
                    for (int i = 0; i < m_catchedComponentList.Count; ++i)
                    {
                        Component compo = m_catchedComponentList[i];
                        if (compo is UIWindowCanvas)
                        {
                            m_canvas = compo as UIWindowCanvas;
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
            }
            return m_canvas;
        }

        private void CollectWidgetInventory()
        {
            m_widgetInventoryMap.Clear();
            UIWidgetInventory[] inventories = GetComponents<UIWidgetInventory>();
            for (int i = 0; i < inventories.Length; ++i)
            {
                UIWidgetInventory inventory = inventories[i];
                if (!m_widgetInventoryMap.ContainsKey(inventory.Id))
                {
                    inventory.parent = this;
                    m_widgetInventoryMap.Add(inventory.Id, inventory);
                }
                else
                {
                    Debug.LogError("Error: UIWidgetInventories have same id in A GameObject:" + gameObject.name);
                }
            }
        }

        public UIWidgetInventory GetInventory(int id = UIWidgetInventory.defaultId)
        {
            if (m_cachedInventory == null || id != m_cachedInventory.Id)
            {
                m_cachedInventory = m_widgetInventoryMap.GetClassValue(id);
            }
            return m_cachedInventory;
        }

        protected void DebugLog(string str)
        {
            Debug.Log(string.Format("UILog: <color=#4BDAFF>{0}</color", str));
        }

        protected virtual void OnInit(object param)
        {
        }

        protected virtual void OnUnInit()
        {
        }

        protected virtual void RegisterListener()
        {
        }

        protected virtual void UnRegisterListener()
        {
        }

        protected virtual void OnSetActive(bool flag, bool isValid)
        {
        }

        protected virtual void Update()
        {
        }

        public virtual void OnWindowSetActive(bool flag, bool isValid)
        {
            for (int i = 0; i < m_componentList.Count; ++i)
            {
                m_componentList[i].OnWindowSetActive(flag, isValid);
            }
        }
    }
}
