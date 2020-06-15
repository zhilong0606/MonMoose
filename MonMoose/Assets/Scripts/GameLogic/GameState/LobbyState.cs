using MonMoose.Core;
using MonMoose.StaticData;

namespace MonMoose.Logic
{
    public class LobbyState : State
    {
        public override int stateIndex
        {
            get { return (int)EGameState.Lobby; }
        }

        protected override void OnEnter(StateContext context)
        {
            UIWindowManager.instance.OpenWindow((int)EWindowId.Lobby);
        }

        protected override void OnExit()
        {
            UIWindowManager.instance.DestroyAllWindow();
        }
    }
}

