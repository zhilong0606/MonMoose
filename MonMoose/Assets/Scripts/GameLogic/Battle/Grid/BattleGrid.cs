using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGrid
{
    public BattleGridView m_view;

    public Grid2D m_pos;
    public BattleGrid[] m_relatedGrids = new BattleGrid[(int)EBattleGridRalationType.Max];

    public BattleGrid upGrid { get { return m_relatedGrids[(int)EBattleGridRalationType.Up]; } }
    public BattleGrid downGrid { get { return m_relatedGrids[(int)EBattleGridRalationType.Down]; } }
    public BattleGrid leftGrid { get { return m_relatedGrids[(int)EBattleGridRalationType.Left]; } }
    public BattleGrid rightGrid { get { return m_relatedGrids[(int)EBattleGridRalationType.Right]; } }
    public BattleGrid[] relatedGrids { get { return m_relatedGrids; } }

    public void Init(BattleGridView view, Grid2D pos)
    {
        m_view = view;
        m_pos = pos;
        for (int i = 0; i < m_relatedGrids.Length; ++i)
        {
            m_relatedGrids[i] = null;
        }
    }

    public void SetRelatedGrid(EBattleGridRalationType type, BattleGrid grid)
    {
        m_relatedGrids[(int)type] = grid;
    }
}
