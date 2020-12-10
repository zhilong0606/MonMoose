using MonMoose.Battle;
using MonMoose.Core;
using MonMoose.Logic.UI;
using MonMoose.StaticData;
using State = MonMoose.Core.State;
using StateContext = MonMoose.Core.StateContext;

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
            EventManager.instance.RegisterListener<int>((int)EventID.BattleStart_StartRequest, OnStartRequestByBtnClick);
        }

        protected override void OnExit()
        {
            EventManager.instance.UnregisterListener<int>((int)EventID.BattleStart_StartRequest, OnStartRequestByBtnClick);
            UIWindowManager.instance.DestroyAllWindow();
        }

        private void OnStartRequestByBtnClick(int id)
        {
            LoadingWindow.OpenLoading(ELoadingId.BattleScene, ELoadingWindowType.FadeBlack, () =>
            {
                BattleStateContext ctx = ClassPoolManager.instance.Fetch<BattleStateContext>(this);
                ctx.battleInitData = GetTestBattleInitData(id);
                m_stateMachine.ChangeState((int)EGameState.Battle, ctx);
            });
        }


        private BattleInitData GetTestBattleInitData(int id)
        {
            BattleInitData battleInitData = new BattleInitData();
            battleInitData.id = id;
            //{
            //    TeamInitData teamInitData = new TeamInitData();
            //    teamInitData.isAI = false;
            //    battleInitData.teamList.Add(teamInitData);
            //}
            return battleInitData;
        }
    }
}

