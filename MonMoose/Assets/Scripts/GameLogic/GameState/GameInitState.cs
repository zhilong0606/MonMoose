using System;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public class GameInitState : State
    {
        public override int stateIndex
        {
            get { return (int)EGameState.GameInit; }
        }

        protected override void OnEnter()
        {
            GameInitializer initializer = new GameInitializer();
            UIWindowManager.instance.OpenWindow((int)EWindowType.GameInitWindow, initializer);
            initializer.StartAsync(OnGameInitEnd);
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
