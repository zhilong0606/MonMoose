using System.Collections;
using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public class BattleStage
    {
        private BattleBase m_battleInstance;
        private BattleStageStaticInfo m_staticInfo;
        private List<BattleGrid> m_gridList = new List<BattleGrid>();
        private int m_width;
        private int m_height;

        public void Init(int id, BattleBase battleInstance)
        {
            m_battleInstance = battleInstance;
            m_staticInfo = StaticDataManager.instance.GetBattleStageStaticInfo(id);
            m_width = m_staticInfo.LeftWidth + m_staticInfo.RightWidth;
            m_height = m_staticInfo.Height;
            for (int i = 0; i < m_staticInfo.GridIdList.Count; ++i)
            {
                BattleGrid grid = new BattleGrid();
                int x = i / m_staticInfo.Height;
                int y = i % m_staticInfo.Height;
                grid.Init(m_staticInfo.GridIdList[i], x, y);
                m_gridList.Add(grid);
            }
        }

        public BattleGrid GetGrid(int x, int y)
        {
            for (int i = 0; i < m_gridList.Count; ++i)
            {
                if (m_gridList[i].gridPos.x == x && m_gridList[i].gridPos.y == y)
                {
                    return m_gridList[i];
                }
            }
            return null;
        }
    }
}
