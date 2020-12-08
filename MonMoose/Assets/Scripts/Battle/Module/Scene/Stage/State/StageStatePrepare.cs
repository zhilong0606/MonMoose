using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public class StageStatePrepare : StageState
    {
        public override int stateIndex
        {
            get { return (int)EStageState.Prepare; }
        }

        protected override void OnEnter(StateContext context)
        {
            base.OnEnter(context);
        }
    }
}
