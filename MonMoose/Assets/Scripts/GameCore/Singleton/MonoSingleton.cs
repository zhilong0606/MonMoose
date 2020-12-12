using UnityEngine;

namespace MonMoose.Core
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        [SerializeField]
        private bool m_dontDestroy;

        private static T m_instance;

        public static T instance
        {
            get
            {
                CreateInstance();
                return m_instance;
            }
        }

        public static void CreateInstance(bool dontDestroy = false)
        {
            if (m_instance == null)
            {
                m_instance.m_dontDestroy = dontDestroy;
                GameObject go = new GameObject(typeof(T).Name);
                CreateInstanceInternal(go);
            }
        }

        public static void DestroyInstance()
        {
            if (m_instance != null)
            {
                Destroy(m_instance);
            }
        }

        public static bool hasInstance()
        {
            return m_instance != null;
        }

        private static void CreateInstanceInternal(GameObject go)
        {
            m_instance = go.GetComponent<T>();
            if (m_instance == null)
            {
                m_instance = go.AddComponent<T>();
            }
            m_instance.OnInit();
            if (m_instance.m_dontDestroy)
            {
                DontDestroyOnLoad(go);
            }
        }

        void Awake()
        {
            if (m_instance == null)
            {
                CreateInstanceInternal(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            if (m_instance != null)
            {
                OnUnInit();
                m_instance = null;
            }
        }

        protected virtual void OnInit()
        {

        }

        protected virtual void OnUnInit()
        {

        }
    }
}