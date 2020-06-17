using MonMoose.Core;
using MonMoose.Logic.UI;
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
            EventManager.instance.RegisterListener((int)EventID.BattleStart_StartRequest_BtnClick, OnStartRequestByBtnClick);
        }

        protected override void OnExit()
        {
            EventManager.instance.UnregisterListener((int)EventID.BattleStart_StartRequest_BtnClick, OnStartRequestByBtnClick);
            UIWindowManager.instance.DestroyAllWindow();
        }

        private void OnStartRequestByBtnClick()
        {
            LoadingWindow.OpenLoading(ELoadingId.BattleScene, ELoadingWindowType.FadeBlack, OnLoadingShowEnd);
        }

        private void OnLoadingShowEnd()
        {
            BattleStateContext ctx = ClassPoolManager.instance.Fetch<BattleStateContext>();
            m_stateMachine.ChangeState((int)EGameState.Battle, ctx);
        }
    }
}

