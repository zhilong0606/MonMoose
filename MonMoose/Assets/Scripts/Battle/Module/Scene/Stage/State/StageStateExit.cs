using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public class StageStateExit : StageState
    {
        public override int stateIndex
        {
            get { return (int)EStageState.Exit; }
        }
    }
}
