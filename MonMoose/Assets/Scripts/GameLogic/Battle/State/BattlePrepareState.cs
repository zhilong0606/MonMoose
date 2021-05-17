using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.GameLogic;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattlePrepareState : State
    {
        private InputHandlerBattlePrepare m_inputHandler = new InputHandlerBattlePrepare();

        public override int stateIndex
        {
            get { return (int)EBattleStageState.Prepare; }
        }

        protected override void OnEnter(StateContext context)
        {
            EventManager.instance.RegisterListener((int)EventID.BattlePrepare_Finish, OnPrepareFinished);
            InputManager.instance.RegisterHandler(m_inputHandler);
            BattlePrepareActorManager.CreateInstance();
        }

        protected override void OnExit()
        {
            EventManager.instance.UnRegisterListener((int)EventID.BattlePrepare_Finish, OnPrepareFinished);
            InputManager.instance.UnRegisterHandler(m_inputHandler);
            BattlePrepareActorManager.DestroyInstance();
        }

        private void OnPrepareFinished()
        {
            ownerMachine.ChangeState((int)EBattleStageState.Main);
        }
    }
}
