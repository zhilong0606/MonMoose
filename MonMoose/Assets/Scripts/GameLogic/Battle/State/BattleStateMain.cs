using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleStateMain : State
    {
        private InputHandlerBattleMain m_inputHandler = new InputHandlerBattleMain();

        public override int stateIndex
        {
            get { return (int)EBattleState.Main; }
        }

        protected override void OnEnter(StateContext context)
        {
            base.OnEnter(context);
            InputManager.instance.RegisterHandler(m_inputHandler);
            EventManager.instance.Broadcast((int)EventID.BattleStart);
            Debug.LogError("战斗开始");
        }

        protected override void OnExit()
        {
            InputManager.instance.UnRegisterHandler(m_inputHandler);
            BattlePrepareActorManager.DestroyInstance();
        }
    }
}
