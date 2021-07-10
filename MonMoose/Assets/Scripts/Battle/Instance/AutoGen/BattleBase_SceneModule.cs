using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.StaticData;
using UnityEngine;

namespace MonMoose.Battle
{
    public partial class BattleBase
    {
        public void StartScene()
        {
            if (m_sceneModule != null)
            {
                m_sceneModule.StartScene();
            }
        }

        public BattleStage GetCurStage()
        {
            if (m_sceneModule != null)
            {
                return m_sceneModule.GetCurStage();
            }
            return default;
        }

        public BattleGrid GetGrid(int x, int y)
        {
            if (m_sceneModule != null)
            {
                return m_sceneModule.GetGrid(x, y);
            }
            return default;
        }

        public BattleGrid GetGrid(GridPosition gridPos)
        {
            if (m_sceneModule != null)
            {
                return m_sceneModule.GetGrid(gridPos);
            }
            return default;
        }
    }
}
