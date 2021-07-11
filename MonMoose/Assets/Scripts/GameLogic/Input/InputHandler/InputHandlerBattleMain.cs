using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using MonMoose.Core;
using MonMoose.GameLogic.Battle;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.GameLogic
{
    public class InputHandlerBattleMain : InputHandler
    {
        protected override void OnHandleClick(PointerEventData eventData)
        {
            GameObject go = eventData.pointerCurrentRaycast.gameObject;
            BattleGridView gridView = go.GetComponent<BattleGridView>();
            if (gridView != null)
            {
                List<Entity> list = new List<Entity>();
                BattleShortCut.battleInstance.GetEntitysByGrid(gridView.gridPosition, list);
                foreach (Entity entity in list)
                {
                    Actor actor = entity as Actor;
                    if (actor != null)
                    {
                        EventManager.instance.Broadcast((int)EventID.Battle_ActorSelected, actor.GetComponent<EntityInfoComponent>().entityId);
                    }
                }
            }
        }
    }
}
