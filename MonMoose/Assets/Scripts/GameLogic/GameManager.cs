using MonMoose.Core;
using UnityEngine;

namespace MonMoose.GameLogic
{
    public enum EGameState
    {
        GameInit,
        Lobby,
        Battle,
    }

    public class GameManager : MonoSingleton<GameManager>
    {
        private StateMachine m_stateMachine = new StateMachine();

        public StateMachine stateMachine
        {
            get { return m_stateMachine; }
        }

        protected override void OnInit()
        {
            m_stateMachine.Init(
                new GameInitState(),
                new LobbyState(),
                new BattleState()
                );
            RegisterListener();
            InitGlobalDefine();
            m_stateMachine.ChangeState((int)EGameState.GameInit);
        }

        protected override void OnUnInit()
        {
            RemoveListener();
            base.OnUnInit();
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
            InputManager.CreateInstance();
            UIWindowDefine.DefineBeforeGameInit();
        }

        private void Update()
        {
            m_stateMachine.Tick();
            TickManager.instance.Tick(Time.deltaTime);
        }
    }
}

