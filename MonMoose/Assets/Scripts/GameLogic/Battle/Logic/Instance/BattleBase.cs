using System.Collections;
using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public class BattleBase
    {
        private List<Team> m_teamList = new List<Team>();
        private List<Entity> m_entityList = new List<Entity>();
        private BattleSceneStaticInfo m_staticInfo;

        private PoolModule m_poolModule = new PoolModule();
        private StageModule m_stageModule = new StageModule();

        public void Init(BattleInitData battleInitData)
        {
            m_battleContext = new BattleContext();
            m_battleContext.battleBase = this;

            m_staticInfo = StaticDataManager.instance.GetBattleSceneStaticInfo(battleInitData.id);
            battleInitData.teamList.Sort(TeamInitData.Sort);
            for (int i = 0; i < battleInitData.teamList.Count; ++i)
            {
                Team team = new Team();

                TeamContext teamContext = new TeamContext();
                teamContext.battleContext = m_battleContext;
                teamContext.team = team;

                team.Init(battleInitData.teamList[i], teamContext);
                m_teamList.Add(team);
            }
        }

        public Grid GetGrid(int x, int y)
        {
            return m_stageModule.GetGrid(x, y);
        }

        public Grid GetGrid(GridPosition gridPos)
        {
            return GetGrid(gridPos.x, gridPos.y);
        }

        public T FetchPoolObj<T>() where T : class { return m_poolModule.Fetch<T>(); }

        public void Tick()
        {
            for (int i = 0; i < m_teamList.Count; ++i)
            {
                m_teamList[i].Tick();
            }
            for (int i = 0; i < m_entityList.Count; ++i)
            {
                m_entityList[i].Tick();
            }
        }
    }
}
