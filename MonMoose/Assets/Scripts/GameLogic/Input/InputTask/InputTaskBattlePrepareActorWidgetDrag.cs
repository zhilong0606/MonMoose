using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.GameLogic.Battle;
using MonMoose.Logic.UI;
using MonMoose.StaticData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.Logic
{
    public class InputTaskBattlePrepareActorWidgetDrag : InputTask
    {
        public BattlePrepareActorItemWidget actorItemWidget;
        private BattlePrepareCoverActorItemWidget m_dragActorItemWidget;
        private bool m_isDraging;

        public override void OnRelease()
        {
            base.OnRelease();
            m_dragActorItemWidget = null;
            m_isDraging = false;
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnHandleDrag(PointerEventData eventData)
        {
            if (m_isDraging)
            {
                m_dragActorItemWidget.SetScreenPos(eventData.position);
            }
        }

        protected override void OnHandleUp(PointerEventData eventData)
        {
            m_isDraging = false;
            m_dragActorItemWidget.SetActiveSafely(false);
            BattleGridView view = BattleTouchSystem.instance.GetGridView(eventData.position);
            if (view == null)
            {
                actorItemWidget.SetActiveSafely(true);
            }
            else
            {
                actorItemWidget.SetActiveSafely(false);
                ActorStaticInfo entityInfo = StaticDataManager.instance.GetActor(actorItemWidget.actorId);
                GameObject go = GameObjectPoolManager.instance.Fetch(entityInfo.PrefabPath);
                go.transform.position = view.transform.position;
                BattlePrepareActorManager.instance.AddActor(actorItemWidget.actorId, go, view.gridPosition);
                EventManager.instance.Broadcast((int)EventID.BattlePrepare_ActorItemHide, actorItemWidget.actorId);
            }
            End();
        }

        protected override void OnHandleExit(PointerEventData eventData)
        {
            if (m_isDown)
            {
                BattlePrepareCoverWindow coverWindow = UIWindowManager.instance.GetWindow<BattlePrepareCoverWindow>((int)EWindowId.BattlePrepareCover);
                m_dragActorItemWidget = coverWindow != null ? coverWindow.actorItemWidget : null;
                m_dragActorItemWidget.SetActiveSafely(true);
                m_dragActorItemWidget.SetActor(actorItemWidget.actorId);
                m_isDraging = true;
            }
        }
    }
}