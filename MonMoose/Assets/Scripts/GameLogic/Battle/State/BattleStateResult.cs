using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleStateResult : State
    {
        private int m_stateIndex;

        public override int stateIndex
        {
            get { return (int)EBattleState.Result; }
        }

        protected override void OnEnter(StateContext context)
        {
            base.OnEnter(context);
        }
    }
}