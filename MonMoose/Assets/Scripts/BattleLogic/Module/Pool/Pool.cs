using System;
using System.Collections.Generic;

namespace MonMoose.BattleLogic
{
    public class Pool
    {
        public delegate void DelegateObject(object obj);

        protected List<PoolObj> m_objList = new List<PoolObj>();
        protected List<PoolObj> m_spareList = new List<PoolObj>();
        protected Type m_classType;

        public virtual Type classType
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

        public Pool(Type type)
        {
            m_classType = type;
        }

        public PoolObj Fetch()
        {
            PoolObj obj;
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
            obj.OnFetch();
            return obj;
        }

        public void Release(PoolObj obj)
        {
            if (!m_objList.Contains(obj))
            {
                //throw new Exception(string.Format("Error: Trying to destroy object that is not create from this pool. {0} => {1}", obj.GetType().Name, classType.Name));
                return;
            }
            if (m_spareList.Contains(obj))
            {
                //throw new Exception("Error: Trying to destroy object that is already released to pool.");
                return;
            }
            m_objList.Remove(obj);
            m_spareList.Add(obj);
            obj.OnRelease();
        }

        private PoolObj CreateObj()
        {
            PoolObj obj = Activator.CreateInstance(classType) as PoolObj;
            if (obj != null)
            {
                obj.creater = this;
            }
            return obj;
        }
    }
}