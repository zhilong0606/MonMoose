using System;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.BattleLogic
{
    public class PoolModule : Module
    {
        private List<ClassPool> m_poolList = new List<ClassPool>();

        public T Fetch<T>() where T : class
        {
            ClassPool pool = GetPool(typeof(T));
            return pool.Fetch() as T;
        }

        public object Fetch(Type type)
        {
            ClassPool pool = GetPool(type);
            return pool.Fetch();
        }

        private ClassPool GetPool(Type type)
        {
            for (int i = 0; i < m_poolList.Count; ++i)
            {
                if (m_poolList[i].classType == type)
                {
                    return m_poolList[i];
                }
            }
            ClassPool pool = new ClassPool(type);
            m_poolList.Add(pool);
            return pool;
        }
    }
}
