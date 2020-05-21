using UnityEngine;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public class GameObjectPool : MonoBehaviour
    {
        public delegate void ActionObjHolder(PoolObjHolder holder);

        public delegate void ActionObjFuncInt(PoolObjHolder holder, int paramInt);

        [SerializeField] private GameObject m_dynamicObj;
        [SerializeField] private GameObject m_apearRoot;
        [SerializeField] private int m_capacity = 0;


        private List<PoolObjHolder> m_holderList = new List<PoolObjHolder>();
        private ActionObjHolder m_actionOnInit;
        private ActionObjHolder m_actionOnFetch;
        private ActionObjHolder m_actionOnRelease;

        public int capacity
        {
            get { return m_capacity; }
            set
            {
                if (value > m_holderList.Count)
                {
                    for (int i = m_holderList.Count; i < value; ++i)
                    {
                        CreatHolder();
                    }
                    m_capacity = value;
                }
            }
        }

        public void Init(GameObject obj, ActionObjHolder actionOnInit = null)
        {
            m_dynamicObj = obj;
            Init(actionOnInit);
        }

        public void Init(GameObject obj, ActionObjHolder actionOnInit, ActionObjHolder actionOnFetch, ActionObjHolder actionOnRelease)
        {
            m_dynamicObj = obj;
            Init(actionOnInit, actionOnFetch, actionOnRelease);
        }

        public void Init(ActionObjHolder actionOnInit = null)
        {
            if (m_dynamicObj != null && transform != m_dynamicObj.transform.parent)
            {
                m_dynamicObj = Instantiate(m_dynamicObj, transform) as GameObject;
            }
            m_dynamicObj.SetActiveSafely(false);
            m_actionOnInit = actionOnInit;
            capacity = m_capacity;
        }

        public void Init(ActionObjHolder actionOnInit, ActionObjHolder actionOnFetch, ActionObjHolder actionOnRelease)
        {
            Init(actionOnInit);
            m_actionOnFetch = actionOnFetch;
            m_actionOnRelease = actionOnRelease;
        }

        public PoolObjHolder Fetch()
        {
            PoolObjHolder holder = GetHolder();
            if (holder != null)
            {
                holder.FetchTo(m_apearRoot != null ? m_apearRoot : gameObject);
                if (m_actionOnFetch != null)
                {
                    m_actionOnFetch(holder);
                }
                return holder;
            }
            return null;
        }

        public T FetchComponent<T>() where T : Component
        {
            PoolObjHolder holder = Fetch();
            if (holder != null)
            {
                if (m_actionOnFetch != null)
                {
                    m_actionOnFetch(holder);
                }
                return holder.GetComponent<T>();
            }
            return null;
        }

        public void Release(GameObject go)
        {
            if (go == null)
            {
                return;
            }
            for (int i = 0; i < m_holderList.Count; ++i)
            {
                if (m_holderList[i].obj == go)
                {
                    if (m_actionOnRelease != null)
                    {
                        m_actionOnRelease(m_holderList[i]);
                    }
                    m_holderList[i].ReleaseTo(gameObject);
                    return;
                }
            }
            Debug.LogError(go.name + "is Not in Pool, But Trying Release this");
        }

        public void ReleaseAll()
        {
            for (int i = 0; i < m_holderList.Count; ++i)
            {
                if (m_actionOnRelease != null)
                {
                    m_actionOnRelease(m_holderList[i]);
                }
                m_holderList[i].ReleaseTo(gameObject);
            }
        }

        public void HandleFunc(ActionObjFuncInt handleFuncInt, int paramInt)
        {
            for (int i = 0; i < m_holderList.Count; ++i)
            {
                if (handleFuncInt != null)
                {
                    handleFuncInt(m_holderList[i], paramInt);
                }
            }
        }

        private PoolObjHolder GetHolder()
        {
            for (int i = 0; i < m_holderList.Count; ++i)
            {
                if (!m_holderList[i].isUsed)
                {
                    return m_holderList[i];
                }
            }
            return CreatHolder();
        }

        private PoolObjHolder CreatHolder()
        {
            PoolObjHolder holder = null;
            if (m_dynamicObj != null)
            {
                GameObject go = Instantiate(m_dynamicObj, transform) as GameObject;
                holder = ClassPoolManager.instance.Fetch<PoolObjHolder>();
                holder.obj = go;
                if (m_actionOnInit != null)
                {
                    m_actionOnInit(holder);
                }
                m_holderList.Add(holder);
            }
            return holder;
        }

        private void OnDestroy()
        {
            for (int i = 0; i < m_holderList.Count; ++i)
            {
                m_holderList[i].Release();
            }
            m_holderList.Clear();
        }

        public class PoolObjHolder : ClassPoolObj
        {
            private GameObject m_obj;
            private List<Component> m_componentList = new List<Component>();
            private bool m_isUsed = false;

            public bool isUsed
            {
                get { return m_isUsed; }
            }

            public GameObject obj
            {
                get { return m_obj; }
                set
                {
                    m_obj = value;
                    CollectComponents();
                }
            }

            public void CollectComponents()
            {
                m_componentList.Clear();
                m_obj.GetComponents(m_componentList);
            }

            public T GetComponent<T>() where T : Component
            {
                int count = 0;
                T component = null;
                for (int i = 0; i < m_componentList.Count; ++i)
                {
                    if (m_componentList[i] is T)
                    {
                        component = m_componentList[i] as T;
                        count++;
                    }
                }
                if (count > 1)
                {
                    Debug.LogError("Error: Component geted is not unique");
                }
                return component;
            }

            public T AddComponent<T>() where T : Component
            {
                T component = m_obj.AddComponent<T>();
                AddComponent(component);
                return component;
            }

            public void AddComponent(Component component)
            {
                if (!m_componentList.Contains(component))
                {
                    m_componentList.Add(component);
                }
            }

            public void FetchTo(GameObject root)
            {
                m_isUsed = true;
                m_obj.SetParent(root);
                m_obj.SetActiveSafely(true);
            }

            public void ReleaseTo(GameObject root)
            {
                m_isUsed = false;
                m_obj.SetActiveSafely(false);
                m_obj.SetParent(root);
            }

            public override void OnRelease()
            {
                m_componentList.Clear();
                m_obj = null;
            }
        }
    }
}
