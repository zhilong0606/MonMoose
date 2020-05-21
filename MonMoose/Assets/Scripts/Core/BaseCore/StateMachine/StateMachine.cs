using System;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public class StateMachine
    {
        private Dictionary<int, State> m_stateMap = new Dictionary<int, State>();
        private State m_curState;

        public State curState
        {
            get { return m_curState; }
        }

        public int curStateIndex
        {
            get { return m_curState.stateIndex; }
        }

        public StateMachine(State[] states)
        {
            for (int i = 0; i < states.Length; ++i)
            {
                State state = states[i];
                m_stateMap.Add(state.stateIndex, state);
            }
        }

        ~StateMachine()
        {
            foreach (var kv in m_stateMap)
            {
                kv.Value.OnUninit();
            }
            m_stateMap.Clear();
            m_curState = null;
        }

        public State GetState(int stateIndex)
        {
            return m_stateMap.GetClassValue(stateIndex);
        }

        public void ChangeState(int stateIndex)
        {
            State state = GetState(stateIndex);
            if (state == null)
            {
                throw new Exception(string.Format("Error: State with Index {0} not exist!!", stateIndex));
            }
            ChangeState(state);
        }

        private void ChangeState(State state)
        {
            if (state == null || m_curState == state)
            {
                return;
            }
            if (m_curState != null)
            {
                m_curState.OnExit();
            }
            m_curState = state;
            m_curState.OnEnter();
        }

        public void TickFloat(float deltaTime)
        {
            if (m_curState != null)
            {
                m_curState.OnTickFloat(deltaTime);
            }
        }

        public void TickInt(int deltaTime)
        {
            if (m_curState != null)
            {
                m_curState.OnTickInt(deltaTime);
            }
        }
    }
}
