﻿using System.Collections;
using System.Collections.Generic;

namespace MonMoose.BattleLogic
{
    public class StageStateRunning : StageState
    {
        public override int stateIndex
        {
            get { return (int)EStageState.Running; }
        }
    }
}
