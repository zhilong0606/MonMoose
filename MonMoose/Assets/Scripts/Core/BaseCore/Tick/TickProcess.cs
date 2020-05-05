using System;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public class TickProcess
    {
        private float m_interval;
        private double m_totalTime;
        private ulong m_tickCount;
        private bool m_isStart;
        private EventGroup<TickProcess> m_eventOnTick = new EventGroup<TickProcess>();

        public float interval
        {
            get { return m_interval; }
        }

        public double totalTime
        {
            get { return m_totalTime; }
        }

        public ulong tickCount
        {
            get { return m_tickCount; }
        }

        public TickProcess(float interval)
        {
            m_interval = interval;
        }

        public void RegisterListener(Action<TickProcess> action)
        {
            m_eventOnTick.Register(action);
        }

        public void UnregisterListener(Action<TickProcess> action)
        {
            m_eventOnTick.Unregister(action);
        }

        public bool ContainsAction(Action<TickProcess> action)
        {
            return m_eventOnTick.Contains(action);
        }

        public void Start()
        {
            m_isStart = true;
        }

        public void Stop()
        {
            m_isStart = false;
        }

        public void Tick(float deltaTime)
        {
            if (!m_isStart)
            {
                return;
            }
            m_totalTime += deltaTime;
            ulong tickCountExpect = (ulong)(m_totalTime / m_interval);
            int count = (int)(tickCountExpect - m_tickCount);
            for (int i = 0; i < count; ++i)
            {
                TickOnce();
            }
        }

        private void TickOnce()
        {
            m_tickCount++;
            m_eventOnTick.Broadcast(this);
        }
    }
}
