using System.Collections;
using System.Collections.Generic;
using MonMoose.Logic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.GameLogic.Battle
{
    public class BattleGridInputComponent : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            InputManager.instance.HandleClick(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            InputManager.instance.HandleDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            InputManager.instance.HandleUp(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InputManager.instance.HandleExit(eventData);
        }
    }
}
