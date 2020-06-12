using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Logic.Battle
{
    public class BattlePrepareState : State
    {
        public override int stateIndex
        {
            get { return (int)EBattleState.Prepare; }
        }
    }
}
