using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public class BattleStageStateExit : BattleStageState
    {
        public override int stateIndex
        {
            get { return (int)EBattleStageState.Exit; }
        }
    }
}
