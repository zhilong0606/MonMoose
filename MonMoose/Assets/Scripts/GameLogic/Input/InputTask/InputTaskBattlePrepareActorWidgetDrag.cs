using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.GameLogic.Battle;
using MonMoose.GameLogic.UI;
using MonMoose.StaticData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.GameLogic
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
            BattleGridView view = BattleShortCut.GetGridViewByScreenPosition(eventData.position);
            if(view != null && view.canEmbattle)
            {
                actorItemWidget.SetActiveSafely(false);
                //BattleShortCut.battleInstance.s()
                BattleShortCut.frameSyncSender.SendFormationtEmbattle(actorItemWidget.actorId, view.gridPosition.x, view.gridPosition.y);
            }
            else
            {
                actorItemWidget.SetActiveSafely(true);
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