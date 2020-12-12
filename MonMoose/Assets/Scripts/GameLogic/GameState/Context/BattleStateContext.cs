using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using UnityEngine;
using StateContext = MonMoose.Core.StateContext;

namespace MonMoose.GameLogic
{
    public class BattleStateContext : StateContext
    {
        public BattleInitData battleInitData;

        public override void OnRelease()
        {
            base.OnRelease();
            battleInitData = null;
        }
    }
}
