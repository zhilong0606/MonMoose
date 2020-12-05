using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.GameLogic.Battle;
using MonMoose.Logic.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.Logic
{
    public class InputHandlerBattlePrepare : InputHandler
    {
        protected override void OnHandleDown(PointerEventData eventData)
        {
            GameObject go = eventData.pointerCurrentRaycast.gameObject;
            BattlePrepareActorItemWidget actorItemWidget = go.GetComponentInParent<BattlePrepareActorItemWidget>();
            if (actorItemWidget != null)
            {
                InputTaskBattlePrepareActorWidgetDrag task = ClassPoolManager.instance.Fetch<InputTaskBattlePrepareActorWidgetDrag>(this);
                task.actorItemWidget = actorItemWidget;
                StartTask(task);
            }
            
            BattleGridView gridView = go.GetComponent<BattleGridView>();
            if (gridView != null)
            {
                int actorId;
                GameObject actorObj;
                if (BattlePrepareActorManager.instance.TryGetActor(gridView.gridPosition, out actorId, out actorObj))
                {
                    InputTaskBattlePrepareEntityViewDrag task = ClassPoolManager.instance.Fetch<InputTaskBattlePrepareEntityViewDrag>(this);
                    task.actorId = actorId;
                    task.actorObj = actorObj;
                    StartTask(task);
                }
            }
        }
    }
}
