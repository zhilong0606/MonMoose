namespace MonMoose.Battle
{
    public abstract class BattleViewController<T> : BattleViewController where T : BattleObj
    {
        protected T m_owner;

        public void Init(T owner)
        {
            m_owner = owner;
        }

        public void UnInit()
        {
            m_owner = null;
        }
    }

    public abstract class BattleViewController
    {
        public abstract void InitView();
        public abstract void UnInitView();

        public void Tick()
        {
            OnTick();
        }

        protected virtual void OnTick()
        {
        }
    }
}