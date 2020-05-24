using System;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.Logic.Battle;
using MonMoose.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MonMoose.Logic
{
    public class BattleState : State
    {
        private BattleBase m_battleInstance;

        public override int stateIndex
        {
            get { return (int)EGameState.Battle; }
        }

        protected override void OnInit()
        {
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
            BattleInitializer initializer = new BattleInitializer();
            m_battleInstance.Init(GetTestBattleInitData());
            initializer.Init(m_battleInstance);
            initializer.StartAsync(OnLoadEnd);
        }

        private void OnLoadEnd()
        {
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
                BattleTeamInitData teamInitData = new BattleTeamInitData();
                teamInitData.isAI = false;
                teamInitData.camp = ECampType.Camp1;
                {
                    ActorInitData actorInitData = new ActorInitData();
                    actorInitData.id = 1;
                    teamInitData.actorList.Add(actorInitData);
                }
                battleInitData.teamList.Add(teamInitData);
            }
            return battleInitData;
        }

        private BattleGrid m_downGrid;

        //public override void OnTickFloat(float deltaTime)
        //{
        //    base.OnTickFloat(deltaTime);
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit hit;
        //        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("BattleGrid")))
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
        //        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("BattleGrid")))
        //        {
        //            BattleGridView view = hit.transform.GetComponent<BattleGridView>();
        //            if (view != null)
        //            {

        //            }
        //        }
        //    }
        //}

        private void OnFrameTick()
        {
        }
    }
}
