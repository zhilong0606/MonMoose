using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public abstract class Entity : BattleObj
    {
        protected int m_uid;
        protected EntityView m_view;
        protected Team m_team;
        protected List<EntityComponent> m_componentList = new List<EntityComponent>();

        public int uid
        {
            get { return m_uid; }
        }

        public EntityView view
        {
            get { return m_view; }
        }

        public void Init(int uid, EntityInitData initData)
        {
            m_uid = uid;

            m_view = m_battleInstance.GetEntityView(initData.id);
            m_view.SetEntity(this);

            m_componentList.Add(m_battleInstance.FetchPoolObj<LocationComponent>());
            m_componentList.Add(m_battleInstance.FetchPoolObj<EntityPrepareComponent>());
            OnInitComponent();
            m_componentList.Sort(EntityComponent.Sort);
            for (int i = 0; i < m_componentList.Count; ++i)
            {
                m_componentList[i].Init(this, initData);
            }
        }

        public void SetTeam(Team team)
        {
            m_team = team;
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

        public static int Sort(Entity x, Entity y)
        {
            return x.uid.CompareTo(y.uid);
        }
    }
}
