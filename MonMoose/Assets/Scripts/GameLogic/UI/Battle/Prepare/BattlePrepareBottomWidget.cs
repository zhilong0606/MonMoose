using System.Collections;
using System.Collections.Generic;
using MonMoose.BattleLogic;
using MonMoose.Core;
using MonMoose.GameLogic.Battle;
using UnityEngine;
using UnityEngine.UI;

namespace MonMoose.Logic.UI
{
    public class BattlePrepareBottomWidget : UIComponent
    {
        private GameObjectPool m_pool;
        private Dictionary<int, BattlePrepareActorItemWidget> m_actorItemMap = new Dictionary<int, BattlePrepareActorItemWidget>();
        private List<Entity> m_entityList = new List<Entity>();

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            m_pool = GetInventory().GetComponent<GameObjectPool>((int)EWidget.Pool);
            m_pool.Init(OnActorItemInit);
            Button startBtn = GetInventory().GetComponent<Button>((int)EWidget.StartBtn);
            startBtn.onClick.AddListener(OnStartBtnClicked);

            int hostTeamId = BattleManager.instance.hostTeamId;
            BattleManager.instance.battleInstance.GetEntityListByTeamId(hostTeamId, m_entityList);
            for (int i = 0; i < m_entityList.Count; ++i)
            {
                BattlePrepareActorItemWidget widget = m_pool.FetchComponent<BattlePrepareActorItemWidget>();
                widget.SetActor(m_entityList[i].GetComponent<ActorInfoComponent>().actorStaticInfo.Id);
                m_actorItemMap.Add(widget.actorId, widget);
            }
        }

        protected override void RegisterListener()
        {
            base.RegisterListener();
            EventManager.instance.RegisterListener<int>((int)EventID.BattlePrepare_ActorItemShow, OnActorItemShow);
            EventManager.instance.RegisterListener<int>((int)EventID.BattlePrepare_ActorItemHide, OnActorItemHide);
        }

        protected override void UnregisterListener()
        {
            base.UnregisterListener();
            EventManager.instance.UnregisterListener<int>((int)EventID.BattlePrepare_ActorItemShow, OnActorItemShow);
            EventManager.instance.UnregisterListener<int>((int)EventID.BattlePrepare_ActorItemHide, OnActorItemHide);
        }

        private void OnActorItemShow(int actorId)
        {
            BattlePrepareActorItemWidget widget;
            if (m_actorItemMap.TryGetValue(actorId, out widget))
            {
                widget.SetActive(true);
            }
        }

        private void OnActorItemHide(int actorId)
        {
            BattlePrepareActorItemWidget widget;
            if (m_actorItemMap.TryGetValue(actorId, out widget))
            {
                widget.SetActive(false);
            }
        }

        private void OnActorItemInit(GameObjectPool.PoolObjHolder holder)
        {
            BattlePrepareActorItemWidget widget = holder.obj.AddComponent<BattlePrepareActorItemWidget>();
            widget.Initialize(this);
            holder.AddComponent(widget);
        }

        private void OnStartBtnClicked()
        {
            EventManager.instance.Broadcast((int)EventID.BattlePrepare_Finish);
        }

        private enum EWidget
        {
            Pool,
            StartBtn,
        }
    }
}
