using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.BattleLogic
{
    public class StageStateNone : StageState
    {
        public override int stateIndex
        {
            get { return (int)EStageState.None; }
        }
    }
}
