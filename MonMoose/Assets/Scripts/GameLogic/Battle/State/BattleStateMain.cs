using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleStateMain : State
    {
        private int m_stateIndex;

        public override int stateIndex
        {
            get { return (int)EBattleState.Main; }
        }

        protected override void OnEnter(StateContext context)
        {
            base.OnEnter(context);
            EventManager.instance.Broadcast((int)EventID.BattleStart);
            Debug.LogError("战斗开始");
        }
    }
}
