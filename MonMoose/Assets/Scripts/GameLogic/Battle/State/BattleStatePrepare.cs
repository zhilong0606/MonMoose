using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.GameLogic;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleStatePrepare : State
    {
        private InputHandlerBattlePrepare m_inputHandler = new InputHandlerBattlePrepare();

        public override int stateIndex
        {
            get { return (int)EBattleState.Prepare; }
        }

        protected override void OnEnter(StateContext context)
        {
            InputManager.instance.RegisterHandler(m_inputHandler);
            BattlePrepareActorManager.CreateInstance();
        }

        protected override void OnExit()
        {
            InputManager.instance.UnRegisterHandler(m_inputHandler);
            BattlePrepareActorManager.DestroyInstance();
        }
    }
}
