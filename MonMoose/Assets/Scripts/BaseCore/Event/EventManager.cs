using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace MonMoose.Core
{
    public class EventManager : Singleton<EventManager>
    {
        private Dictionary<int, List<Delegate>> m_delegateListMap = new Dictionary<int, List<Delegate>>();

        protected override void UnInit()
        {
            m_delegateListMap.Clear();
        }

        public void RegisterListener(int eventId, Delegate d)
        {
            if (d == null)
            {
                return;
            }
            List<Delegate> list = GetDelegateList(eventId, true);
            if (list.Count > 0 && d.GetType() != list[0].GetType())
            {
                throw new Exception(string.Format("Error: {0} Has More Than One ParamGroups!!!", eventId));
            }
            if (!list.Contains(d))
            {
                list.Add(d);
            }
        }

        public void RegisterListener(int eventId, Action action)
        {
            RegisterListener(eventId, action as Delegate);
        }

        public void RegisterListener<T1>(int eventId, Action<T1> action)
        {
            RegisterListener(eventId, action as Delegate);
        }

        public void RegisterListener<T1, T2>(int eventId, Action<T1, T2> action)
        {
            RegisterListener(eventId, action as Delegate);
        }

        public void RegisterListener<T1, T2, T3>(int eventId, Action<T1, T2, T3> action)
        {
            RegisterListener(eventId, action as Delegate);
        }

        public void RegisterListener<T1, T2, T3, T4>(int eventId, Action<T1, T2, T3, T4> action)
        {
            RegisterListener(eventId, action as Delegate);
        }

        public void UnregisterListener(int eventId, Delegate d)
        {
            if (d == null)
            {
                return;
            }
            List<Delegate> list = GetDelegateList(eventId);
            if (list != null)
            {
                list.Remove(d);
            }
        }

        public void UnregisterListener(int eventId, Action action)
        {
            UnregisterListener(eventId, action as Delegate);
        }

        public void UnregisterListener<T1>(int eventId, Action<T1> action)
        {
            UnregisterListener(eventId, action as Delegate);
        }

        public void UnregisterListener<T1, T2>(int eventId, Action<T1, T2> action)
        {
            UnregisterListener(eventId, action as Delegate);
        }

        public void UnregisterListener<T1, T2, T3>(int eventId, Action<T1, T2, T3> action)
        {
            UnregisterListener(eventId, action as Delegate);
        }

        public void UnregisterListener<T1, T2, T3, T4>(int eventId, Action<T1, T2, T3, T4> action)
        {
            UnregisterListener(eventId, action as Delegate);
        }

        private void BroadCastInternal(int eventId, Predicate<Delegate> handle)
        {
            if (eventId < 0)
            {
                return;
            }
            bool findNotMatch = false;
            List<Delegate> list;
            if (m_delegateListMap.TryGetValue(eventId, out list))
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    if (!handle(list[i]))
                    {
                        findNotMatch = true;
                    }
                }
            }
            if (findNotMatch)
            {
                throw new Exception(string.Format("Error: Find Event {0} Not Match ParamGroups!!!", eventId));
            }
        }

        public void BroadCast(int eventId)
        {
            List<Delegate> list = GetDelegateList(eventId);
            if (list == null)
            {
                return;
            }
            bool findNotMatch = false;
            Exception cachedException = null;
            Action action;
            for (int i = 0; i < list.Count; ++i)
            {
                action = list[i] as Action;
                if (action != null)
                {
                    try
                    {
                        action();
                    }
                    catch (Exception e)
                    {
                        if (cachedException == null)
                        {
                            cachedException = e;
                        }
                    }
                }
                else
                {
                    findNotMatch = true;
                }
            }
            HandleExceptionOnBroadcast(eventId, findNotMatch, cachedException);
        }

        public void BroadCast<T1>(int eventId, T1 arg1)
        {
            List<Delegate> list = GetDelegateList(eventId);
            if (list == null)
            {
                return;
            }
            bool findNotMatch = false;
            Exception cachedException = null;
            Action<T1> action;
            for (int i = 0; i < list.Count; ++i)
            {
                action = list[i] as Action<T1>;
                if (action != null)
                {
                    try
                    {
                        action(arg1);
                    }
                    catch (Exception e)
                    {
                        if (cachedException == null)
                        {
                            cachedException = e;
                        }
                    }
                }
                else
                {
                    findNotMatch = true;
                }
            }
            HandleExceptionOnBroadcast(eventId, findNotMatch, cachedException);
        }

        public void BroadCast<T1, T2>(int eventId, T1 arg1, T2 arg2)
        {
            List<Delegate> list = GetDelegateList(eventId);
            if (list == null)
            {
                return;
            }
            bool findNotMatch = false;
            Exception cachedException = null;
            Action<T1, T2> action;
            for (int i = 0; i < list.Count; ++i)
            {
                action = list[i] as Action<T1, T2>;
                if (action != null)
                {
                    try
                    {
                        action(arg1, arg2);
                    }
                    catch (Exception e)
                    {
                        if (cachedException == null)
                        {
                            cachedException = e;
                        }
                    }
                }
                else
                {
                    findNotMatch = true;
                }
            }
            HandleExceptionOnBroadcast(eventId, findNotMatch, cachedException);
        }

        public void BroadCast<T1, T2, T3>(int eventId, T1 arg1, T2 arg2, T3 arg3)
        {
            List<Delegate> list = GetDelegateList(eventId);
            if (list == null)
            {
                return;
            }
            bool findNotMatch = false;
            Exception cachedException = null;
            Action<T1, T2, T3> action;
            for (int i = 0; i < list.Count; ++i)
            {
                action = list[i] as Action<T1, T2, T3>;
                if (action != null)
                {
                    try
                    {
                        action(arg1, arg2, arg3);
                    }
                    catch (Exception e)
                    {
                        if (cachedException == null)
                        {
                            cachedException = e;
                        }
                    }
                }
                else
                {
                    findNotMatch = true;
                }
            }
            HandleExceptionOnBroadcast(eventId, findNotMatch, cachedException);
        }

        public void BroadCast<T1, T2, T3, T4>(int eventId, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            List<Delegate> list = GetDelegateList(eventId);
            if (list == null)
            {
                return;
            }
            bool findNotMatch = false;
            Exception cachedException = null;
            Action<T1, T2, T3, T4> action;
            for (int i = 0; i < list.Count; ++i)
            {
                action = list[i] as Action<T1, T2, T3, T4>;
                if (action != null)
                {
                    try
                    {
                        action(arg1, arg2, arg3, arg4);
                    }
                    catch (Exception e)
                    {
                        if (cachedException == null)
                        {
                            cachedException = e;
                        }
                    }
                }
                else
                {
                    findNotMatch = true;
                }
            }
            HandleExceptionOnBroadcast(eventId, findNotMatch, cachedException);
        }

        public string GetAllDelegateRefLog(Type enumType = null)
        {
            string str = string.Empty;
            Dictionary<int, List<Delegate>>.Enumerator enumerator = m_delegateListMap.GetEnumerator();
            List<Type> cacheList = new List<Type>();
            while (enumerator.MoveNext())
            {
                int key = enumerator.Current.Key;
                List<Delegate> value = enumerator.Current.Value;
                if (value.Count > 0)
                {
                    cacheList.Clear();
                    for (int i = 0; i < value.Count; ++i)
                    {
                        Type type = value[i].Target.GetType();
                        if (!cacheList.Contains(type))
                        {
                            cacheList.Add(type);
                        }
                    }
                    str += enumType == null ? key.ToString() : Enum.GetName(enumType, key);
                    for (int i = 0; i < cacheList.Count; ++i)
                    {
                        str += ",";
                        str += cacheList[i].Name;
                    }
                }
                str += "\r\n";
            }
            enumerator.Dispose();
            return str;
        }

        private void HandleExceptionOnBroadcast(int eventId, bool findNotMatch, Exception cachedException)
        {
            if (findNotMatch)
            {
                throw new Exception(string.Format("Error: Find Event [{0}] Not Match ParamGroups!!!", eventId));
            }
            if (cachedException != null)
            {
                throw cachedException;
            }
        }

        private List<Delegate> GetDelegateList(int eventId, bool autoCreate = false)
        {
            if (eventId < 0)
            {
                return null;
            }
            List<Delegate> list;
            if (!m_delegateListMap.TryGetValue(eventId, out list) && autoCreate)
            {
                list = new List<Delegate>();
                m_delegateListMap.Add(eventId, list);
            }
            return list;
        }
    }
}
