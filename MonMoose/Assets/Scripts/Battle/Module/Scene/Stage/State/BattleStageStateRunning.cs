using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class BattleStageStateRunning : BattleStageState
    {
        public override int stateIndex
        {
            get { return (int)EBattleStageState.Running; }
        }
    }
}
