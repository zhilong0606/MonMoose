using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Logic.Battle
{
    public class StageModule : Module
    {
        private List<Stage> m_stageList = new List<Stage>();
        private int m_curStageIndex = 0;

        public Stage GetCurStage()
        {
            if (m_curStageIndex >= 0 && m_curStageIndex < m_stageList.Count)
            {
                return m_stageList[m_curStageIndex];
            }
            return null;
        }

        public Grid GetGrid(int x, int y)
        {
            Stage curStage = GetCurStage();
            if (curStage != null)
            {
                return curStage.GetGrid(x, y);
            }
            return null;
        }

        public Grid GetGrid(GridPosition gridPos)
        {
            return GetGrid(gridPos.x, gridPos.y);
        }
    }
}
