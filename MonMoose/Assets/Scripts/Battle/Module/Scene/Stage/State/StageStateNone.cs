using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public class StageStateNone : StageState
    {
        public override int stateIndex
        {
            get { return (int)EStageState.None; }
        }
    }
}
