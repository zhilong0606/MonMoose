using MonMoose.Core;

namespace MonMoose.Logic
{
    public class LobbyState : State
    {
        public override int stateIndex
        {
            get { return (int)EGameState.Lobby; }
        }

        public override void OnEnter()
        {
            UIWindowManager.instance.OpenWindow((int)EWindowType.LobbyWindow);
        }

        public override void OnExit()
        {
            UIWindowManager.instance.DestroyAllWindow();
        }
    }
}

