namespace MonMoose.Core
{
    public abstract class State
    {
        public abstract int stateIndex { get; }

        public virtual void OnInit()
        {
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual void OnUninit()
        {
        }

        public virtual void OnTickFloat(float deltaTime)
        {
        }

        public virtual void OnTickInt(int deltaTime)
        {
        }
    }
}