using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class StageStatePrepare : StageState
    {
        public override int stateIndex
        {
            get { return (int)EStageState.Prepare; }
        }
    }
}
