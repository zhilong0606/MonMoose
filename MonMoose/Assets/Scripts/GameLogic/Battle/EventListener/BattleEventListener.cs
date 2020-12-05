using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleEventListener : IBattleEventListener
    {
        public void StagePrepare()
        {
            BattleManager.instance.StagePrepare();
        }
    }
}
