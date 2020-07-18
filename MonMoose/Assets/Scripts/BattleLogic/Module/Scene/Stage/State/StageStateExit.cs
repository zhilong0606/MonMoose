using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.BattleLogic
{
    public class StageStateExit : StageState
    {
        public override int stateIndex
        {
            get { return (int)EStageState.Exit; }
        }
    }
}
