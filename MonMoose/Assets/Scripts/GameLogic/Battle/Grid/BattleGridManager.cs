using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.Logic.Battle;
using UnityEngine;

namespace MonMoose.Logic.Battle
{
    public class BattleGridManager : Singleton<BattleGridManager>
    {
        private List<BattleGridView> m_gridList = new List<BattleGridView>();

        public void AddGridView(BattleGridView grid)
        {
            m_gridList.Add(grid);
        }

        public BattleGridView GetGridView(GridPosition pos)
        {
            for (int i = 0; i < m_gridList.Count; ++i)
            {
                BattleGridView gridView = m_gridList[i];
                if (gridView.gridPosition == pos)
                {
                    return gridView;
                }
            }
            return null;
        }

        public Vector3 GetWorldPosition(GridPosition gridPos, FixVec2 offset)
        {
            BattleGridView gridView = GetGridView(gridPos);
            if (gridView != null)
            {
                return gridView.transform.position + new Vector3((float)offset.x, 0f, (float)offset.y);
            }
            return Vector3.zero;
        }
    }
}