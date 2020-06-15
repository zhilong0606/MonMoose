using System;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public class StateMachine
    {
        private List<State> m_stateList = new List<State>();
        private State m_curState;

        public State curState
        {
            get { return m_curState; }
        }

        public int curStateIndex
        {
            get { return m_curState.stateIndex; }
        }

        public void Init(params State[] states)
        {
            for (int i = 0; i < states.Length; ++i)
            {
                State state = states[i];
                state.Init(this);
                m_stateList.Add(state);
            }
        }

        public void Uninit()
        {
            for (int i = 0; i < m_stateList.Count; ++i)
            {
                m_stateList[i].Uninit();
            }
            m_stateList.Clear();
            m_curState = null;
        }

        public State GetState(int stateIndex)
        {
            for (int i = 0; i < m_stateList.Count; ++i)
            {
                if (m_stateList[i].stateIndex == stateIndex)
                {
                    return m_stateList[i];
                }
            }
            return null;
        }

        public void ChangeState(int stateIndex, StateContext context = null)
        {
            State state = GetState(stateIndex);
            if (state == null)
            {
                throw new Exception(string.Format("Error: State with Index {0} not exist!!", stateIndex));
            }
            ChangeState(state, context);
        }

        public void ChangeState(State state, StateContext context = null)
        {
            if (m_curState != null)
            {
                m_curState.Exit();
            }
            if (state == null || m_curState == state)
            {
                return;
            }
            m_curState = state;
            m_curState.Enter(context);
        }

        public void Tick()
        {
            if (m_curState != null)
            {
                m_curState.Tick();
            }
        }
    }
}
