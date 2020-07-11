namespace MonMoose.BattleLogic
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

        public void Enter(StateContext context)
        {
            m_isRunning = true;
            OnEnter(context);
            if (context != null)
            {
                context.Release();
            }
        }

        public void Exit()
        {
            OnExit();
            m_isRunning = false;
        }

        public void Tick()
        {
            OnTick();
        }

        protected virtual void OnInit() { }
        protected virtual void OnUninit() { }
        protected virtual void OnEnter(StateContext context) { }
        protected virtual void OnExit() { }
        protected virtual void OnTick() { }
    }
}