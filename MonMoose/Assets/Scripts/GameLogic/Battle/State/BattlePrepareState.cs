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

        protected override void OnEnter(StateContext context)
        {
            EventManager.instance.RegisterListener((int)EventID.BattlePrepare_Finish, OnPrepareFinished);
            InputManager.instance.RegisterHandler(m_inputHandler);
            BattlePrepareActorManager.CreateInstance();
        }

        protected override void OnExit()
        {
            EventManager.instance.UnregisterListener((int)EventID.BattlePrepare_Finish, OnPrepareFinished);
            InputManager.instance.UnregisterHandler(m_inputHandler);
            BattlePrepareActorManager.DestroyInstance();
        }

        private void OnPrepareFinished()
        {
            //for(int i=0;i<BattlePrepareActorManager.instance.)
            m_stateMachine.ChangeState((int)EBattleState.Main);
        }
    }
}
