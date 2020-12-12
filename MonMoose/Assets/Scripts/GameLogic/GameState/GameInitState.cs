using System;
using MonMoose.Core;
using MonMoose.GameLogic.UI;

namespace MonMoose.GameLogic
{
    public class GameInitState : State
    {
        public override int stateIndex
        {
            get { return (int)EGameState.GameInit; }
        }

        protected override void OnEnter(StateContext context)
        {
            GameInitParam param = new GameInitParam();
            param.initializer = new GameInitializer();
            param.actionOnInitEnd = OnGameInitEnd;
            UIWindowManager.instance.OpenWindow((int)EWindowId.GameInit, param);
        }

        protected override void OnExit()
        {
            UIWindowManager.instance.DestroyAllWindow();
        }

        private void OnGameInitEnd()
        {
            UIWindowDefine.DefineAfterGameInit();
            m_stateMachine.ChangeState((int)EGameState.Lobby);
        }
    }
}
