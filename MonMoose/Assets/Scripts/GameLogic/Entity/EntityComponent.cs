namespace MonMoose.Logic
{
    public class EntityComponent
    {
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
