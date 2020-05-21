using MonMoose.Core;

namespace MonMoose.Logic
{
    public class LobbyState : State
    {
        public override int stateIndex
        {
            get { return (int)EGameState.Lobby; }
        }

        protected override void OnEnter()
        {
            UIWindowManager.instance.OpenWindow((int)EWindowId.LobbyWindow);
        }

        protected override void OnExit()
        {
            UIWindowManager.instance.DestroyAllWindow();
        }
    }
}

