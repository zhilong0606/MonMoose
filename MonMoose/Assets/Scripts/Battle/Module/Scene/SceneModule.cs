using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.StaticData;
using UnityEngine;

namespace MonMoose.Battle
{
    public class SceneModule : Module
    {
        private BattleSceneStaticInfo m_staticInfo;
        private List<BattleStage> m_stageList = new List<BattleStage>();
        private int m_curStageIndex = 0;

        protected override void OnInit(BattleInitData battleInitData)
        {
            base.OnInit(battleInitData);
            m_staticInfo = StaticDataManager.instance.GetBattleScene(battleInitData.id);
            for (int i = 0; i < m_staticInfo.StageIdList.Count; ++i)
            {
                int stageRid = m_staticInfo.StageIdList[i];
                BattleStage stage = m_battleInstance.FetchPoolObj<BattleStage>(this);
                stage.Init(stageRid);
                m_stageList.Add(stage);
            }
        }

        protected override void OnTick()
        {
            BattleStage curStage = GetCurStage();
            if (curStage != null)
            {
                curStage.Tick();
            }
        }

        public void Start()
        {
            m_curStageIndex = 0;
            GetCurStage().Enter();
            GetCurStage().Start();
        }

        [ShortCutMethod(true)]
        public BattleStage GetCurStage()
        {
            if (m_curStageIndex >= 0 && m_curStageIndex < m_stageList.Count)
            {
                return m_stageList[m_curStageIndex];
            }
            return null;
        }

        [ShortCutMethod(true)]
        public BattleGrid GetGrid(int x, int y)
        {
            BattleStage curStage = GetCurStage();
            if (curStage != null)
            {
                return curStage.GetGrid(x, y);
            }
            return null;
        }

        [ShortCutMethod(true)]
        public BattleGrid GetGrid(GridPosition gridPos)
        {
            return GetGrid(gridPos.x, gridPos.y);
        }
    }
}
