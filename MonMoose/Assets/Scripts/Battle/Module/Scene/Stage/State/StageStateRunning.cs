using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class StageStateRunning : StageState
    {
        public override int stateIndex
        {
            get { return (int)EStageState.Running; }
        }
    }
}
