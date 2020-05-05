using System;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public class ClassPoolManager : Singleton<ClassPoolManager>
    {
        private Dictionary<Type, ClassPool> m_poolMap = new Dictionary<Type, ClassPool>();

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

        public void Release(object obj)
        {
            ClassPool pool = GetPool(obj.GetType());
            pool.Release(obj);
        }

        public void SetCapacity(Type type, int capacity)
        {
            ClassPool pool = GetPool(type);
            pool.capacity = capacity;
        }

        public void RegisterOnFetch(Type type, ClassPool.DelegateObject action)
        {
            ClassPool pool = GetPool(type);
            pool.actionOnFetch += action;
        }

        public void UnregisterOnFetch(Type type, ClassPool.DelegateObject action)
        {
            ClassPool pool = GetPool(type);
            pool.actionOnFetch -= action;
        }

        public void RegisterOnRelease(Type type, ClassPool.DelegateObject action)
        {
            ClassPool pool = GetPool(type);
            pool.actionOnRelease += action;
        }

        public void UnregisterOnRelease(Type type, ClassPool.DelegateObject action)
        {
            ClassPool pool = GetPool(type);
            pool.actionOnRelease -= action;
        }

        private ClassPool GetPool(Type type)
        {
            ClassPool pool = null;
            if (m_poolMap.TryGetValue(type, out pool))
            {
                pool = new ClassPool(type);
                m_poolMap.Add(type, pool);
            }
            return pool;
        }
    }
}
