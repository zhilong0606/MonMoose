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

        private void Update()
        {
            m_stateMachine.Tick();
            TickManager.instance.Tick(Time.deltaTime);
        }
    }
}

