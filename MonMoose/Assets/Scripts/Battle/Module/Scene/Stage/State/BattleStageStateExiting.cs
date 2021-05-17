using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class BattleStageStateExiting : BattleStageState
    {
        public override int stateIndex
        {
            get { return (int)EBattleStageState.Exiting; }
        }
    }
}
