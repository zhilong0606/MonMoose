using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public class BattleGridManager : Singleton<BattleGridManager>
    {
        public List<BattleGrid> m_gridList = new List<BattleGrid>();

        protected override void OnUninit()
        {
            ClearGrid();
        }

        public void AddGrid(BattleGrid grid)
        {
            if (!m_gridList.Contains(grid))
            {
                m_gridList.Add(grid);
            }
        }

        public void RemoveGrid(BattleGrid grid)
        {
            m_gridList.Remove(grid);
        }

        public void ClearGrid()
        {
            m_gridList.Clear();
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

        public BattleGrid GetGrid(Grid2D gridPos)
        {
            return GetGrid(gridPos.x, gridPos.y);
        }



    }
}
