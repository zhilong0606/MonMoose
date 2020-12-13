using System;
using System.Collections.Generic;
using MonMoose.Battle;
using MonMoose.Core;
using MonMoose.GameLogic.Battle;
using MonMoose.GameLogic.UI;
using MonMoose.StaticData;
using UnityEngine;

namespace MonMoose.GameLogic
{
    public class BattleState : State
    {
        private BattleBase m_battleInstance;
        private Stage m_curStage;
        private BattleScene m_battleScene;
        private StateMachine m_stateMachine = new StateMachine();
        private BattleInitData m_battleInitData;
        private FrameSyncSender m_sender;
        private bool m_isLoadEnd;

        public override int stateIndex
        {
            get { return (int)EGameState.Battle; }
        }

        public BattleScene battleScene
        {
            get { return m_battleScene; }
        }

        public Stage curStage
        {
            get { return m_curStage; }
        }

        public StateMachine stateMachine
        {
            get { return m_stateMachine; }
        }

        protected override void OnInit()
        {
            m_stateMachine.Init(
                new BattlePrepareState(),
                new BattleMainState()
            );
            RegisterListener();
        }

        protected override void OnUnInit()
        {
            RemoveListener();
        }

        protected override void OnEnter(StateContext context)
        {
            BattleStateContext battleStateContext = context as BattleStateContext;
            if (battleStateContext != null)
            {
                m_battleInitData = battleStateContext.battleInitData;
                m_battleInitData.funcOnCreateCtrl = OnCreateCtrl;
                m_battleInstance = new BattleBase();
                m_battleInitData.relay = new FrameSyncRelayLocal();
                m_battleInitData.eventListener = new BattleEventListener();
                BattleInitializer initializer = new BattleInitializer();
                initializer.StartAsync(OnLoadEnd);
            }
        }

        private void OnLoadEnd(Initializer initializer)
        {
            BattleInitializer battleInitializer = initializer as BattleInitializer;
            m_battleScene = battleInitializer.battleScene;
            m_isLoadEnd = true;
            m_battleInstance.Init(m_battleInitData);
            m_battleInstance.Start();
            m_sender = m_battleInstance.sender;
            LoadingWindow.CloseLoading(ELoadingId.BattleScene);
        }

        protected override void OnExit()
        {
        }

        private void RegisterListener()
        {
            EventManager.instance.RegisterListener((int)EventID.Frame_Tick, OnFrameTick);
            EventManager.instance.RegisterListener<Stage>((int)EventID.BattleStage_SetActive, OnStageActive);
        }

        private void RemoveListener()
        {
            EventManager.instance.UnregisterListener((int)EventID.Frame_Tick, OnFrameTick);
            EventManager.instance.UnregisterListener<Stage>((int)EventID.BattleStage_SetActive, OnStageActive);
        }

        private void OnStageActive(Stage stage)
        {
            if (m_curStage != null)
            {
                Debug.LogError("Error: Cur Stage is Already Exist.");
            }
            m_curStage = stage;
        }

        private BattleViewController OnCreateCtrl(EBattleViewControllerType type)
        {
            string causer = "BattleState.OnCreateCtrl";
            switch (type)
            {
                case EBattleViewControllerType.Entity:
                    return m_battleInstance.FetchPoolObj<EntityViewController>(causer);
                case EBattleViewControllerType.Grid:
                    return m_battleInstance.FetchPoolObj<BattleGridController>(causer);
                case EBattleViewControllerType.Stage:
                    return m_battleInstance.FetchPoolObj<BattleStageController>(causer);
            }
            return null;
        }

        private BattleGrid m_downGrid;

        //protected override void OnTick()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit hit;
        //        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Grid")))
        //        {
        //            BattleGridView view = hit.transform.GetComponent<BattleGridView>();
        //            if (view != null)
        //            {
        //                m_downGrid = BattleGridManager.instance.GetGrid(view.position);
        //            }
        //        }
        //    }

        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit hit;
        //        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Grid")))
        //        {
        //            BattleGridView view = hit.transform.GetComponent<BattleGridView>();
        //            if (view != null)
        //            {

        //            }
        //        }
        //    }
        //}

        public void Send(KeyCode code)
        {
            switch (code)
            {
                case KeyCode.W:
                    m_sender.SendMoveToGrid(10001, 1, 4);
                    break;
                case KeyCode.S:
                    m_sender.SendMoveToGrid(10001, 1, 3);
                    break;
                case KeyCode.A:
                    m_sender.SendMoveToGrid(10001, 0, 3);
                    break;
                case KeyCode.D:
                    m_sender.SendMoveToGrid(10001, 7, 2);
                    break;
                case KeyCode.X:
                    m_sender.SendMoveToGrid(10001, 1, 1);
                    break;
            }
        }

        private void OnFrameTick()
        {
        }

        protected override void OnTick()
        {
            base.OnTick();
            if (m_isLoadEnd)
            {
                m_battleInstance.Tick(Time.deltaTime);
            }
        }
    }
}
