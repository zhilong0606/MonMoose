using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Logic.Battle
{
    public class BattleMainState : State
    {
        private int m_stateIndex;

        public override int stateIndex
        {
            get { return (int)EBattleState.Main; }
        }
    }
}
