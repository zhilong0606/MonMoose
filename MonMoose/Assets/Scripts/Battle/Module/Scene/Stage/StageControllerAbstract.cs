using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public abstract class StageControllerAbstract : BattleViewController<Stage>
    {
        public abstract void StartLoadScene(Action actionOnEnd);
    }
}
