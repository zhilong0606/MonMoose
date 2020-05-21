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

        public override void OnEnter()
        {
            UIWindowManager.instance.OpenWindow((int)EWindowType.GameInitWindow, (Action)OnGameInitEnd);
        }

        public override void OnExit()
        {
            UIWindowManager.instance.DestroyAllWindow();
        }

        private void OnGameInitEnd()
        {
        }
    }
}
