using System;
using System.Collections.Generic;
using MonMoose.Battle;
using MonMoose.Core;
using MonMoose.GameLogic.Battle;
using MonMoose.Logic.UI;
using MonMoose.StaticData;
using UnityEngine;

namespace MonMoose.Logic
{
    public class BattleState : State
    {
        private BattleBase m_battleInstance;
        private StateMachine m_battleStateMachine = new StateMachine();
        private BattleInitData m_battleInitData;
        private FrameSyncSender m_sender;
        private bool m_isLoadEnd;

        public override int stateIndex
        {
            get { return (int)EGameState.Battle; }
        }

        protected override void OnInit()
        {
            m_battleStateMachine.Init(
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
                BattleManager.CreateInstance();
                m_battleInstance = new BattleBase();
                BattleManager.instance.SetBattleInstance(m_battleInstance);
                m_battleInitData.relay = new FrameSyncRelayLocal();
                BattleInitializer initializer = new BattleInitializer();
                initializer.StartAsync(OnLoadEnd);
            }
        }

        private void OnLoadEnd()
        {
            m_isLoadEnd = true;
            m_battleInstance.Init(m_battleInitData);
            m_battleInstance.Start();
            m_sender = m_battleInstance.sender;
            LoadingWindow.CloseLoading(ELoadingId.BattleScene);
        }

        protected override void OnExit()
        {
            BattleManager.DestroyInstance();
        }

        private void RegisterListener()
        {
            EventManager.instance.RegisterListener((int)EventID.Frame_Tick, OnFrameTick);
        }

        private void RemoveListener()
        {
            EventManager.instance.UnregisterListener((int)EventID.Frame_Tick, OnFrameTick);
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
