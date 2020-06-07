using System;
using MonMoose.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.Logic.UI
{
    public class BattlePrepareActorItemWidget : BattlePrepareActorItemBaseWidget, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerExitHandler
    {
        private bool m_isDown;

        public Action<int> actionOnDragStart;

        public void OnPointerDown(PointerEventData eventData)
        {
            m_isDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_isDown = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_isDown)
            {
                m_isDown = false;
                actionOnDragStart(m_actorId);
            }
        }
    }
}
