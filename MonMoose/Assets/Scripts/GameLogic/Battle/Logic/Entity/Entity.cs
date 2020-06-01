using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public abstract class Entity : BattleObj
    {
        protected int m_uid;
        protected int m_entityRid;
        protected EntityView m_view;
        protected List<EntityComponent> m_componentList = new List<EntityComponent>();

        public int uid
        {
            get { return m_uid; }
        }

        public int entityRid
        {
            get { return m_entityRid; }
        }

        public EntityView view
        {
            get { return m_view; }
        }

        public void Init(int uid, int rid)
        {
            m_uid = uid;
            m_entityRid = rid;
            OnInitComponent();
            m_view = m_battleInstance.GetEntityView(rid);
            for (int i = 0; i < m_componentList.Count; ++i)
            {
                m_componentList[i].Init(this);
            }
            m_view.CreateView();
            m_view.SetPosition(new FixVec2(1f, 1f));
        }

        public T GetComponent<T>() where T : EntityComponent
        {
            for (int i = 0; i < m_componentList.Count; ++i)
            {
                T component = m_componentList[i] as T;
                if (component != null)
                {
                    return component;
                }
            }
            return null;
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
