using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using MonMoose.Core;
using MonMoose.GameLogic.Battle;
using MonMoose.GameLogic.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.GameLogic
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
                List<Entity> list = new List<Entity>();
                BattleShortCut.battleInstance.GetEntitysByGrid(gridView.gridPosition, list);
                Actor actor = list.Find(e => e is Actor) as Actor;
                if (actor != null)
                //if (BattlePrepareActorManager.instance.TryGetActor(gridView.gridPosition, out actorId, out actorObj))
                {
                    InputTaskBattlePrepareEntityViewDrag task = ClassPoolManager.instance.Fetch<InputTaskBattlePrepareEntityViewDrag>(this);
                    task.actorId = actor.GetComponent<EntityInfoComponent>().entityId;
                    task.actorObj = (actor.ctrl as EntityViewController).view.gameObject;
                    StartTask(task);
                }
            }
        }
    }
}
