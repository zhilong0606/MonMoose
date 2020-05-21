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
        private StateMachine stateMachine = new StateMachine();

        protected override void OnInit()
        {
            stateMachine.Init(
                new GameInitState(),
                new LobbyState(),
                new BattleState()
                );
            RegisterListener();
            InitGlobalDefine();
            stateMachine.ChangeState((int)EGameState.GameInit);
        }

        protected override void OnUninit()
        {
            RemoveListener();
            base.OnUninit();
        }

        private void RegisterListener()
        {
        }

        private void RemoveListener()
        {
        }

        private void InitGlobalDefine()
        {
            EventManager.CreateInstance();
            UIWindowManager.CreateInstance();
            ResourceManager.CreateInstance();
            TimerManager.CreateInstance();
            UIWindowDefine.Define();
        }

        public void EnterBattle()
        {
            stateMachine.ChangeState((int)EGameState.Battle);
        }

        private void Update()
        {
            TickManager.instance.Tick(Time.deltaTime);
        }
    }
}

