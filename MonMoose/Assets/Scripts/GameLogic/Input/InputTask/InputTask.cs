using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.Logic
{
    public class InputTask : Task
    {
        protected bool m_isDown;
        protected bool m_isIn;

        public override void OnRelease()
        {
            base.OnRelease();
            m_isDown = false;
            m_isIn = false;
        }

        public void HandleClick(PointerEventData eventData)
        {
            OnHandleClick(eventData);
        }
        public void HandleDown(PointerEventData eventData)
        {
            m_isDown = true;
            OnHandleDown(eventData);
        }
        public void HandleUp(PointerEventData eventData)
        {
            m_isDown = false;
            OnHandleUp(eventData);
        }
        public void HandleEnter(PointerEventData eventData)
        {
            m_isIn = true;
            OnHandleEnter(eventData);
        }
        public void HandleExit(PointerEventData eventData)
        {
            m_isIn = false;
            OnHandleExit(eventData);
        }
        public void HandleDrag(PointerEventData eventData)
        {
            OnHandleDrag(eventData);
        }

        protected virtual void OnHandleClick(PointerEventData eventData)
        {
        }
        protected virtual void OnHandleDown(PointerEventData eventData)
        {
        }
        protected virtual void OnHandleUp(PointerEventData eventData)
        {
        }
        protected virtual void OnHandleEnter(PointerEventData eventData)
        {
        }
        protected virtual void OnHandleExit(PointerEventData eventData)
        {
        }
        protected virtual void OnHandleDrag(PointerEventData eventData)
        {
        }
    }
}
