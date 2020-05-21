using System.Collections.Generic;

namespace MonMoose.Logic
{
    public class Entity
    {
        protected EntityView m_view;

        public EntityView view
        {
            get { return m_view; }
        }

        protected List<EntityComponent> m_componentList = new List<EntityComponent>();

        public void Init()
        {
            for (int i = 0; i < m_componentList.Count; ++i)
            {
                m_componentList[i].Init(this);
            }
        }

        public void Tick()
        {
            for (int i = 0; i < m_componentList.Count; ++i)
            {
                m_componentList[i].Tick();
            }
        }
    }
}
