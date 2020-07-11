using System.Collections;
using System.Collections.Generic;
using MonMoose.BattleLogic;
using UnityEngine;
using StateContext = MonMoose.Core.StateContext;

namespace MonMoose.Logic
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
