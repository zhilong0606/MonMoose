﻿using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.GameLogic.Battle;
using MonMoose.GameLogic.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.GameLogic
{
    public class InputTaskBattlePrepareEntityViewDrag : InputTask
    {
        public int actorId;
        public GameObject actorObj;
        private BattlePrepareCoverActorItemWidget m_dragActorItemWidget;
        private bool m_isDraging;
        private int m_dragFingerId;

        public override void OnRelease()
        {
            base.OnRelease();
            m_dragActorItemWidget = null;
            m_isDraging = false;
            m_dragFingerId = 0;
        }

        protected override void OnHandleDrag(PointerEventData eventData)
        {
            if (m_isDraging)
            {
                m_dragActorItemWidget.SetScreenPos(eventData.position);
                m_dragFingerId = eventData.pointerId;
            }
        }

        protected override void OnHandleUp(PointerEventData eventData)
        {
            m_isDraging = false;
            m_dragActorItemWidget.SetActiveSafely(false);
            BattleGridView view = BattleShortCut.GetGridViewByScreenPosition(eventData.position);
            if (view != null && view.canEmbattle)
            {
                //BattleShortCut.frameSyncSender.SendFormationExchange(view.gridPosition.x, view.gridPosition.y);
                actorObj.SetActive(true);
                actorObj.transform.position = view.transform.position;
                BattlePrepareActorManager.instance.RemoveActor(actorId);
                BattlePrepareActorManager.instance.AddActor(actorId, actorObj, view.gridPosition);
            }
            else
            {
                BattleShortCut.frameSyncSender.SendFormationRetreat(actorId);
                GameObjectPoolManager.instance.Release(actorObj);
                EventManager.instance.Broadcast((int)EventID.BattlePrepare_ActorItemShow, actorId);
            }
            End();
        }

        protected override void OnHandleExit(PointerEventData eventData)
        {
            if (m_isDown)
            {
                actorObj.SetActive(false);
                BattlePrepareCoverWindow coverWindow = UIWindowManager.instance.GetWindow<BattlePrepareCoverWindow>((int)EWindowId.BattlePrepareCover);
                m_dragActorItemWidget = coverWindow != null ? coverWindow.actorItemWidget : null;
                m_dragActorItemWidget.SetActiveSafely(true);
                m_dragActorItemWidget.SetActor(actorId);
                m_isDraging = true;
            }
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);
            if (m_isDraging)
            {
                m_dragActorItemWidget.SetScreenPos(InputUtility.GetTouchScreenPos(m_dragFingerId));
            }
        }
    }
}
