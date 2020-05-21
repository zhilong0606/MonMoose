using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Logic
{
    public enum EGameState
    {
        GameInit,
        Lobby,
        Battle,
    }

    public class GameManager : MonoSingleton<GameManager>
    {
        private GameInitializer m_initializer = new GameInitializer();
        private StateMachine stateMachine;

        protected override void Init()
        {
            stateMachine = new StateMachine(new State[]
            {
                new GameInitState(),
                new LobbyState(),
                new BattleState(),
            });
            RegisterListener();
            m_initializer.StartAsync(OnInitFinish);
        }

        private void OnInitFinish()
        {
            stateMachine.ChangeState((int)EGameState.GameInit);
            stateMachine.ChangeState((int)EGameState.Lobby);
        }

        private void RegisterListener()
        {
        }

        private void RemoveListener()
        {
        }

        public void EnterBattle()
        {
            stateMachine.ChangeState((int)EGameState.Battle);
        }

        private void Update()
        {
            TickManager.instance.Tick(Time.deltaTime);
            stateMachine.TickFloat(Time.deltaTime);
        }

        protected override void UnInit()
        {
            RemoveListener();
            base.UnInit();
        }
    }
}

