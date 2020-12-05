using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class StageStateExiting : StageState
    {
        public override int stateIndex
        {
            get { return (int)EStageState.Exiting; }
        }
    }
}
