﻿using System.Collections;
using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public class Stage : BattleObj
    {
        private BattleStageStaticInfo m_staticInfo;
        private List<Grid> m_gridList = new List<Grid>();
        private int m_gridWidth;
        private int m_gridHeight;
        private FixVec2 m_stageSize;

        public void Init(int id)
        {
            m_staticInfo = StaticDataManager.instance.GetBattleStageStaticInfo(id);
            m_gridWidth = m_staticInfo.LeftWidth + m_staticInfo.RightWidth;
            m_gridHeight = m_staticInfo.Height;
            for (int i = 0; i < m_staticInfo.GridIdList.Count; ++i)
            {
                Grid grid = m_battleInstance.FetchPoolObj<Grid>();
                Fix32 gridSize = new Fix32(1);
                int gridPosX = i / m_staticInfo.Height;
                int gridPosY = i % m_staticInfo.Height;
                Fix32 stagePosX = gridSize / 2 + gridSize * gridPosX;
                Fix32 stagePosY = gridSize / 2 + gridSize * gridPosY;
                grid.Init(m_staticInfo.GridIdList[i], new GridPosition(gridPosX, gridPosY), new FixVec2(stagePosX, stagePosY), gridSize);
                m_gridList.Add(grid);
            }
        }

        public Grid GetGrid(int x, int y)
        {
            for (int i = 0; i < m_gridList.Count; ++i)
            {
                if (m_gridList[i].gridPosition.x == x && m_gridList[i].gridPosition.y == y)
                {
                    return m_gridList[i];
                }
            }
            return null;
        }
    }
}
