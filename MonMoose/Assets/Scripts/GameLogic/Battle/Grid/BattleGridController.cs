using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleGridController : BattleGridControllerAbstract
    {
        private BattleGridView m_view;

        public override void InitView()
        {
            m_view = BattleShortCut.GetGridView(m_owner.gridPosition);
        }

        public override void UnInitView()
        {
            m_view = null;
        }

        public override void SetColor(float r, float g, float b)
        {
            if (m_view == null)
            {
                return;
            }
            m_view.SetColor(new Color(r, g, b));
        }
    }
}
