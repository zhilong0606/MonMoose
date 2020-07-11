using MonMoose.BattleLogic;
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
            ctx.battleInitData = GetTestBattleInitData();
            m_stateMachine.ChangeState((int)EGameState.Battle, ctx);
        }


        private BattleInitData GetTestBattleInitData()
        {
            BattleInitData battleInitData = new BattleInitData();
            battleInitData.id = 1;
            {
                TeamInitData teamInitData = new TeamInitData();
                teamInitData.isAI = false;
                teamInitData.camp = ECampType.Camp1;
                {
                    EntityInitData entityInitData = new EntityInitData();
                    entityInitData.id = 1;
                    entityInitData.pos = new GridPosition(7, 2);
                    teamInitData.actorList.Add(entityInitData);
                }
                battleInitData.teamList.Add(teamInitData);
            }
            return battleInitData;
        }
    }
}

