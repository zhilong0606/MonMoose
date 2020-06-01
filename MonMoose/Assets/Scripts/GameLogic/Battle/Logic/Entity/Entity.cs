using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public abstract class Entity : BattleObj
    {
        protected int m_uid;
        protected int m_entityRid;
        protected int m_specificInfoRid;
        protected List<EntityComponent> m_componentList = new List<EntityComponent>();

        public int uid
        {
            get { return m_uid; }
        }

        public int entityRid
        {
            get { return m_entityRid; }
        }

        public void Init(int uid, int rid)
        {
            m_uid = uid;
            m_entityRid = rid;
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
