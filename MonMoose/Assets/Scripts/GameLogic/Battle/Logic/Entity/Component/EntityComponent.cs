namespace MonMoose.Logic.Battle
{
    public abstract class EntityComponent : BattleObj
    {
        public abstract EEntityComponentType type { get; }

        protected Entity m_entity;

        public void Init(Entity entity)
        {
            m_entity = entity;
            OnInit();
        }

        public void Tick()
        {
            OnTick();
        }

        protected virtual void OnInit()
        {

        }

        protected virtual void OnTick()
        {

        }
    }
}
