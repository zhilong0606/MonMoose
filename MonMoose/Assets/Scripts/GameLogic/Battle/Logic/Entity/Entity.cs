using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public abstract class Entity
    {
        protected int m_rid;
        protected int m_uid;
        protected List<EntityComponent> m_componentList = new List<EntityComponent>();

        public int uid
        {
            get { return m_uid; }
        }

        public int rid
        {
            get { return m_rid; }
        }

        public void Init(int uid, int rid)
        {
            m_uid = uid;
            m_rid = rid;
            OnInitComponent();
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

        protected virtual void OnInitComponent()
        {
            
        }
    }
}
