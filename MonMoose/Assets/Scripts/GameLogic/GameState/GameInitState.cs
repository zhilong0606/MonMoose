using System;
using MonMoose.Core;
using MonMoose.Logic.UI;

namespace MonMoose.Logic
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
            m_stateMachine.ChangeState((int)EGameState.Lobby);
        }
    }
}
