using System.Collections;
using System.Collections.Generic;
using MonMoose.StaticData;
using UnityEngine;

namespace MonMoose.BattleLogic
{
    public class SceneModule : Module
    {
        private BattleSceneStaticInfo m_staticInfo;
        private List<Stage> m_stageList = new List<Stage>();
        private int m_curStageIndex = 0;

        protected override void OnInit(BattleInitData battleInitData)
        {
            base.OnInit(battleInitData);
            m_staticInfo = StaticDataManager.instance.GetBattleSceneStaticInfo(battleInitData.id);
            for (int i = 0; i < m_staticInfo.StageIdList.Count; ++i)
            {
                int stageRid = m_staticInfo.StageIdList[i];
                Stage stage = m_battleInstance.FetchPoolObj<Stage>();
                stage.Init(stageRid);
                m_stageList.Add(stage);
            }
        }

        protected override void OnTick()
        {
            Stage curStage = GetCurStage();
            if (curStage != null)
            {
                curStage.Tick();
            }
        }

        public void Start()
        {
            m_curStageIndex = 0;
            GetCurStage().Enter();
        }

        public Stage GetCurStage()
        {
            if (m_curStageIndex >= 0 && m_curStageIndex < m_stageList.Count)
            {
                return m_stageList[m_curStageIndex];
            }
            return null;
        }

        public BattleGrid GetGrid(int x, int y)
        {
            Stage curStage = GetCurStage();
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
    }
}
