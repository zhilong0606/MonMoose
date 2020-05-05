namespace MonMoose.Core
{
    public class Singleton<T> where T : Singleton<T>, new()
    {
        private static readonly object m_locker = new object();
        private static T m_instance;

        public static T instance
        {
            get
            {
                CreateInstance();
                return m_instance;
            }
        }

        public static bool HasInstance
        {
            get { return m_instance != null; }
        }

        public static void CreateInstance()
        {
            if (m_instance == null)
            {
                lock (m_locker)
                {
                    if (m_instance == null)
                    {
                        m_instance = new T();
                        m_instance.Init();
                    }
                }
            }
        }

        public static void DestroyInstance()
        {
            if (m_instance != null)
            {
                m_instance.UnInit();
                m_instance = null;
            }
        }

        protected virtual void Init()
        {

        }

        protected virtual void UnInit()
        {

        }
    }
}