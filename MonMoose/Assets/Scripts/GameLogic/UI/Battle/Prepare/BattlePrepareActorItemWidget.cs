using System;
using MonMoose.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.Logic.UI
{
    public class BattlePrepareActorItemWidget : BattlePrepareActorItemBaseWidget, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerExitHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            InputManager.instance.HandleDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            InputManager.instance.HandleUp(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            InputManager.instance.HandleDrag(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InputManager.instance.HandleExit(eventData);
        }
    }
}
