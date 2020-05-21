namespace MonMoose.Core
{
    public abstract class State
    {
        protected StateMachine m_stateMachine;
        protected bool m_isInited;
        protected bool m_isRunning;

        public abstract int stateIndex { get; }
        public StateMachine stateMachine { get { return m_stateMachine; } }

        public void Init(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
            m_isInited = true;
            OnInit();
        }

        public void Uninit()
        {
            OnUninit();
            m_isInited = false;
        }

        public void Enter()
        {
            m_isRunning = true;
            OnEnter();
        }

        public void Exit()
        {
            OnExit();
            m_isRunning = false;
        }

        protected virtual void OnInit() { }
        protected virtual void OnUninit() { }
        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
    }
}