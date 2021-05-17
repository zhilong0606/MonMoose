using System;
using MonMoose.Core;
using MonMoose.GameLogic.UI;

namespace MonMoose.GameLogic
{
    public class GameStateInit : State
    {
        public override int stateIndex
        {
            get { return (int)EGameState.Init; }
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
            m_ownerMachine.ChangeState((int)EGameState.Lobby);
        }
    }
}
