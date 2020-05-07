using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

public class BattleGridManager : Singleton<BattleGridManager>
{
    public List<Grid> m_gridList = new List<Grid>();

    protected override void UnInit()
    {
        ClearGrid();
    }

    public void AddGrid(Grid grid)
    {
        if (!m_gridList.Contains(grid))
        {
            m_gridList.Add(grid);
        }
    }

    public void RemoveGrid(Grid grid)
    {
        m_gridList.Remove(grid);
    }

    public void ClearGrid()
    {
        m_gridList.Clear();
    }
}
