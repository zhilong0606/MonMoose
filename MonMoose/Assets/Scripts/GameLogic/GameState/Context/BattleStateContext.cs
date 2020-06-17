using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.Logic.Battle;
using UnityEngine;

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
