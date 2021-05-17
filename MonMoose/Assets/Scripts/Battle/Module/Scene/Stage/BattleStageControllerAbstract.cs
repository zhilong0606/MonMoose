using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public abstract class BattleStageControllerAbstract : BattleViewController<BattleStage>
    {
        public abstract void StartLoadScene(Action actionOnEnd);
    }
}
