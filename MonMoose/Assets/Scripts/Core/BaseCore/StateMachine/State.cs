namespace MonMoose.Core
{
    public abstract class State
    {
        protected StateMachine m_stateMachine;
        public abstract int stateIndex { get; }
        public StateMachine stateMachine { get { return m_stateMachine; } }

        public void Init(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
            OnInit();
        }

        public void Uninit()
        {
            OnUninit();
        }

        public void Enter()
        {
            OnEnter();
        }

        public void Exit()
        {
            OnExit();
        }

        public void Tick()
        {
            
        }

        protected virtual void OnInit() { }
        protected virtual void OnUninit() { }
        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }

        public virtual void OnTickFloat(float deltaTime)
        {
        }

        public virtual void OnTickInt(int deltaTime)
        {
        }
    }
}