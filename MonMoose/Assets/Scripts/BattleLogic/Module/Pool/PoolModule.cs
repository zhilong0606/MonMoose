using System;
using System.Collections.Generic;

namespace MonMoose.BattleLogic
{
    public class PoolModule : Module
    {
        private List<Pool> m_poolList = new List<Pool>();

        public T Fetch<T>() where T : class
        {
            Pool pool = GetPool(typeof(T));
            return pool.Fetch() as T;
        }

        public object Fetch(Type type)
        {
            Pool pool = GetPool(type);
            return pool.Fetch();
        }

        private Pool GetPool(Type type)
        {
            for (int i = 0; i < m_poolList.Count; ++i)
            {
                if (m_poolList[i].classType == type)
                {
                    return m_poolList[i];
                }
            }
            Pool pool = new Pool(type);
            m_poolList.Add(pool);
            return pool;
        }
    }
}
