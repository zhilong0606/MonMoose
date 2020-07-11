namespace MonMoose.BattleLogic
{
    public abstract class EntityComponent : BattleObj
    {
        public abstract EEntityComponentType type { get; }

        protected Entity m_entity;

        public void Init(Entity entity, EntityInitData entityInitData)
        {
            m_entity = entity;
            OnInit(entityInitData);
        }

        public void Tick()
        {
            OnTick();
        }

        protected virtual void OnInit(EntityInitData entityInitData)
        {

        }

        protected virtual void OnTick()
        {

        }

        public static int Sort(EntityComponent x, EntityComponent y)
        {
            return x.type.CompareTo(y.type);
        }
    }
}
