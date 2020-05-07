using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGrid
{
    public BattleGridView m_view;

    public Grid2D m_pos;

    public void Init(BattleGridView view, Grid2D pos)
    {
        m_view = view;
        m_pos = pos;
    }

}
