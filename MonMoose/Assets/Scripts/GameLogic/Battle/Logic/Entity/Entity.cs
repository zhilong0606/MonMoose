using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public abstract class Entity
    {
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
