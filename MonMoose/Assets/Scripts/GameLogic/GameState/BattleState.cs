using System;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.Logic.Battle;
using MonMoose.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Grid = MonMoose.Logic.Battle.Grid;

namespace MonMoose.Logic
{
    public class BattleState : State
    {
        private BattleBase m_battleInstance;
        private StateMachine m_battleStateMachine = new StateMachine();
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

        protected override void OnUninit()
        {
            RemoveListener();
        }

        protected override void OnEnter()
        {
            BattleManager.CreateInstance();
            m_battleInstance = new BattleBase();
            m_sender = m_battleInstance.GetSender();
            BattleInitializer initializer = new BattleInitializer();
            initializer.StartAsync(OnLoadEnd);
        }

        private void OnLoadEnd()
        {
            m_isLoadEnd = true;
            m_battleInstance.Init(GetTestBattleInitData());
            m_battleInstance.Start();
            m_battleStateMachine.ChangeState((int)EBattleState.Prepare);
            EventManager.instance.Broadcast((int)EventID.LoadingWindow_FadeOutRequest);
        }

        protected override void OnExit()
        {
            BattleManager.DestroyInstance();
        }

        private void RegisterListener()
        {
            EventManager.instance.RegisterListener((int)EventID.BattleStart_StartRequest_BtnClick, OnStartRequestByBtnClick);
            EventManager.instance.RegisterListener((int)EventID.Frame_Tick, OnFrameTick);
        }

        private void RemoveListener()
        {
            EventManager.instance.UnregisterListener((int)EventID.BattleStart_StartRequest_BtnClick, OnStartRequestByBtnClick);
            EventManager.instance.UnregisterListener((int)EventID.Frame_Tick, OnFrameTick);
        }

        private void OnStartRequestByBtnClick()
        {
            EventManager.instance.Broadcast((int)EventID.LoadingWindow_FadeInRequest, (Action)OnLoadingShowEnd);
        }

        private void OnLoadingShowEnd()
        {
            m_stateMachine.ChangeState((int)EGameState.Battle);
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

                    //entityInitData = new EntityInitData();
                    //entityInitData.id = 1;
                    //entityInitData.pos = new GridPosition(6, 3);
                    //teamInitData.actorList.Add(entityInitData);
                    //entityInitData = new EntityInitData();
                    //entityInitData.id = 1;
                    //entityInitData.pos = new GridPosition(6, 2);
                    //teamInitData.actorList.Add(entityInitData);

                    //entityInitData = new EntityInitData();
                    //entityInitData.id = 1;
                    //entityInitData.pos = new GridPosition(3, 0);
                    //teamInitData.actorList.Add(entityInitData);

                    //entityInitData = new EntityInitData();
                    //entityInitData.id = 1;
                    //entityInitData.pos = new GridPosition(3, 1);
                    //teamInitData.actorList.Add(entityInitData);

                    ////entityInitData = new EntityInitData();
                    ////entityInitData.id = 1;
                    ////entityInitData.pos = new GridPosition(3, 3);
                    ////teamInitData.actorList.Add(entityInitData);

                    //entityInitData = new EntityInitData();
                    //entityInitData.id = 1;
                    //entityInitData.pos = new GridPosition(2, 0);
                    //teamInitData.actorList.Add(entityInitData);
                }
                battleInitData.teamList.Add(teamInitData);
            }
            battleInitData.funcOnGetView = OnGetView;
            return battleInitData;
        }

        private EntityView OnGetView(int id)
        {
            EntityStaticInfo entityStaticInfo = StaticDataManager.instance.GetEntityStaticInfo(id);
            switch (entityStaticInfo.EntityType)
            {
                case EEntityType.Actor:
                    return m_battleInstance.FetchPoolObj<ActorView>();
            }
            return null;
        }

        private Grid m_downGrid;

        //public override void OnTickFloat(float deltaTime)
        //{
        //    base.OnTickFloat(deltaTime);
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
                    m_sender.SendMoveToGrid(10001, new GridPosition(1, 4));
                    break;
                case KeyCode.S:
                    m_sender.SendMoveToGrid(10001, new GridPosition(1, 3));
                    break;
                case KeyCode.A:
                    m_sender.SendMoveToGrid(10001, new GridPosition(0, 3));
                    break;
                case KeyCode.D:
                    m_sender.SendMoveToGrid(10001, new GridPosition(7, 2));
                    break;
                case KeyCode.X:
                    m_sender.SendMoveToGrid(10001, new GridPosition(1, 1));
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
