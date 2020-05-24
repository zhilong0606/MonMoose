using System.Collections;
using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public class BattleBase
    {
        private List<BattleTeam> m_teamList = new List<BattleTeam>();
        private List<BattleStage> m_stageList = new List<BattleStage>();
        private BattleSceneStaticInfo m_staticInfo;
        private int m_curStageIndex = 0;

        public BattleStage curStage
        {
            get
            {
                if (m_curStageIndex >= 0 && m_curStageIndex < m_stageList.Count)
                {
                    return m_stageList[m_curStageIndex];
                }
                return null;
            }
        }

        public void Init(BattleInitData battleInitData)
        {
            m_staticInfo = StaticDataManager.instance.GetBattleSceneStaticInfo(battleInitData.id);
            battleInitData.teamList.Sort(BattleTeamInitData.Sort);
            for (int i = 0; i < battleInitData.teamList.Count; ++i)
            {
                BattleTeam battleTeam = new BattleTeam();
                battleTeam.Init(battleInitData.teamList[i]);
            }
        }

        public void Start()
        {
            m_curStageIndex = 0;
        }

        public BattleGrid GetGrid(int x, int y)
        {
            if (curStage != null)
            {
                return curStage.GetGrid(x, y);
            }
            return null;
        }

        public BattleGrid GetGrid(GridPosition gridPos)
        {
            return GetGrid(gridPos.x, gridPos.y);
        }

        public void Tick()
        {
            for (int i = 0; i < m_teamList.Count; ++i)
            {
                m_teamList[i].Tick();
            }
        }
    }
}
