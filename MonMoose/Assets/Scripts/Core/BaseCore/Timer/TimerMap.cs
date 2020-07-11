﻿using System;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public class TimerMap
    {
        private Dictionary<int, Timer> m_timerMap = new Dictionary<int, Timer>();
        private List<int> m_finishList = new List<int>();
        public Action<int> onTimeUp;
        public Action<int> onAddTimer;
        public Action<int> onRemoveTimer;
        public ClassPool<Timer> pool;

        ~TimerMap()
        {
            Dictionary<int, Timer>.Enumerator enumerator = m_timerMap.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Value != null)
                {
                    enumerator.Current.Value.Release();
                }
            }
            enumerator.Dispose();
            m_timerMap.Clear();
            onTimeUp = null;
        }

        public bool HasTimer(int id)
        {
            Timer timer;
            return m_timerMap.TryGetValue(id, out timer) && timer != null;
        }

        public Timer GetTimer(int id, bool force = false)
        {
            Timer timer = null;
            if (!m_timerMap.TryGetValue(id, out timer) || timer == null)
            {
                if (force)
                {
                    timer = AddTimer(id);
                }
            }
            return timer;
        }

        private Timer CreateTimer()
        {
            if (pool != null)
            {
                return pool.Fetch();
            }
            return new Timer();
        }

        private Timer AddTimer(int id)
        {
            Timer timer = null;
            if (m_timerMap.ContainsKey(id))
            {
                if (m_timerMap[id] == null)
                {
                    timer = CreateTimer();
                    m_timerMap[id] = timer;
                }
            }
            else
            {
                timer = CreateTimer();
                m_timerMap.Add(id, timer);
            }
            if (timer != null)
            {
                onAddTimer(id);
            }
            return timer;
        }

        private void RemoveTimer(Timer timer)
        {
            RemoveTimer(timer.id);
        }

        private void RemoveTimer(int id)
        {
            Timer timer;
            if (m_timerMap.TryGetValue(id, out timer) && timer != null)
            {
                if (onRemoveTimer != null)
                {
                    onRemoveTimer(id);
                }
                timer.Release();
                m_timerMap[id] = null;
            }
        }

        public void Start(int id, float time, Action actionTimeUp = null)
        {
            Timer timer = GetTimer(id, true);
            timer.Start(time, actionTimeUp);
        }

        public void Stop(int id)
        {
            Timer timer = GetTimer(id);
            if (timer != null)
            {
                timer.Stop();
                RemoveTimer(id);
            }
        }

        public void Pause(int id)
        {
            Timer timer = GetTimer(id);
            if (timer != null)
            {
                timer.Pause();
            }
        }

        public void Resume(int id)
        {
            Timer timer = GetTimer(id);
            if (timer != null)
            {
                timer.Resume();
            }
        }

        public void Finish(int id)
        {
            Timer timer = GetTimer(id);
            if (timer != null)
            {
                timer.Finish();
            }
        }

        public bool IsActive(int id)
        {
            Timer timer;
            return m_timerMap.TryGetValue(id, out timer) && timer != null && timer.isActive;
        }

        public float CurTime(int id)
        {
            Timer timer = GetTimer(id);
            if (timer != null)
            {
                return timer.curTime;
            }
            return 0f;
        }

        public float TarTime(int id)
        {
            Timer timer = GetTimer(id);
            if (timer != null)
            {
                return timer.targetTime;
            }
            return 0f;
        }

        public float LeftTime(int id)
        {
            Timer timer = GetTimer(id);
            if (timer != null)
            {
                return timer.leftTime;
            }
            return 0f;
        }

        public void Tick(float deltaTime)
        {
            Dictionary<int, Timer>.Enumerator enumerator = m_timerMap.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Timer timer = enumerator.Current.Value;
                if (timer != null)
                {
                    timer.Tick(deltaTime);
                    if (timer.isFinished && !m_finishList.Contains(enumerator.Current.Key))
                    {
                        m_finishList.Add(enumerator.Current.Key);
                    }
                }
            }
            enumerator.Dispose();
            if (m_finishList.Count > 0)
            {
                for (int i = 0; i < m_finishList.Count; ++i)
                {
                    if (onTimeUp != null)
                    {
                        onTimeUp(m_finishList[i]);
                    }
                    RemoveTimer(m_finishList[i]);
                }
                m_finishList.Clear();
            }
        }
    }
}