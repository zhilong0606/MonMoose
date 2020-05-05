using System;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public class ClassPool<T> : ClassPool
        where T : class
    {
        public ClassPool()
            : base(typeof(T))
        {
        }

        protected ClassPool(Type type) : base(type)
        {
        }

        public new T Fetch()
        {
            return base.Fetch() as T;
        }
    }

    public class ClassPool
    {
        public delegate void DelegateObject(object obj);

        protected List<object> m_objList = new List<object>();
        protected Type m_classType;

        public event DelegateObject actionOnFetch;
        public event DelegateObject actionOnRelease;
        private List<object> m_spareList = new List<object>();

        protected virtual Type classType
        {
            get { return m_classType; }
        }

        public int capacity
        {
            set
            {
                int totalCount = m_objList.Count + m_spareList.Count;
                if (value > totalCount)
                {
                    m_spareList.Capacity = value;
                    m_objList.Capacity = value;
                    int newCount = value - m_objList.Count;
                    for (int i = 0; i < newCount; ++i)
                    {
                        m_spareList.Add(CreateObj());
                    }
                }
            }
        }

        protected ClassPool()
        {
        }

        public ClassPool(Type type)
        {
            m_classType = type;
        }

        public object Fetch()
        {
            object obj;
            if (m_spareList.Count > 0)
            {
                obj = m_spareList[m_spareList.Count - 1];
                m_spareList.RemoveAt(m_spareList.Count - 1);
            }
            else
            {
                obj = CreateObj();
            }
            m_objList.Add(obj);
            IClassPoolObj poolObj = obj as IClassPoolObj;
            if (poolObj != null)
            {
                poolObj.OnFetch();
            }
            if (actionOnFetch != null)
            {
                actionOnFetch(obj);
            }
            return obj;
        }

        public void Release(object obj)
        {
            if (!m_objList.Contains(obj))
            {
                throw new Exception(string.Format("Error: Trying to destroy object that is not create from this pool. {0} => {1}", obj.GetType().Name, classType.Name));
            }
            if (m_spareList.Contains(obj))
            {
                throw new Exception("Error: Trying to destroy object that is already released to pool.");
            }
            IClassPoolObj poolObj = obj as IClassPoolObj;
            if (poolObj != null)
            {
                poolObj.OnRelease();
            }
            if (actionOnRelease != null)
            {
                actionOnRelease(obj);
            }
            m_objList.Remove(obj);
            m_spareList.Add(obj);
        }

        private object CreateObj()
        {
            object obj = Activator.CreateInstance(classType);
            IClassPoolObj poolObj = obj as IClassPoolObj;
            if (poolObj != null)
            {
                poolObj.creater = this;
            }
            return obj;
        }
    }
}