using MonMoose.Battle;
using MonMoose.Core;
using MonMoose.GameLogic.UI;
using MonMoose.StaticData;
using State = MonMoose.Core.State;
using StateContext = MonMoose.Core.StateContext;

namespace MonMoose.GameLogic
{
    public class GameStateLobby : State
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
            EventManager.instance.UnRegisterListener<int>((int)EventID.BattleStart_StartRequest, OnStartRequestByBtnClick);
            UIWindowManager.instance.DestroyAllWindow();
        }

        private void OnStartRequestByBtnClick(int id)
        {
            LoadingWindow.OpenLoading(ELoadingId.BattleScene, ELoadingWindowType.FadeBlack, () =>
            {
                GameStateContextBattle ctx = ClassPoolManager.instance.Fetch<GameStateContextBattle>(this);
                ctx.battleInitData = GetTestBattleInitData(id);
                m_ownerMachine.ChangeState((int)EGameState.Battle, ctx);
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

