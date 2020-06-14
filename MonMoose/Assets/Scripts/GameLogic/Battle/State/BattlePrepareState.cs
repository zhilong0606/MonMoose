using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Logic.Battle
{
    public class BattlePrepareState : State
    {
        private InputHandlerBattlePrepare m_inputHandler = new InputHandlerBattlePrepare();

        public override int stateIndex
        {
            get { return (int)EBattleState.Prepare; }
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            InputManager.instance.RegisterHandler(m_inputHandler);
        }

        protected override void OnExit()
        {
            base.OnExit();
            InputManager.instance.UnregisterHandler(m_inputHandler);
        }
    }
}
