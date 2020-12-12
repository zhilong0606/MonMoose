using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.GameLogic
{
    public class InputManager : Singleton<InputManager>
    {
        private InputHandler m_defaultHandler = new InputHandlerDefault();
        private InputHandler m_curHandler;

        protected override void OnInit()
        {
            base.OnInit();
            m_curHandler = m_defaultHandler;
        }

        public void RegisterHandler(InputHandler handler)
        {
            m_curHandler = handler;
        }

        public void UnregisterHandler(InputHandler handler)
        {
            if (m_curHandler == handler)
            {
                m_curHandler = m_defaultHandler;
            }
        }

        public void HandleDown(PointerEventData eventData)
        {
            m_curHandler.HandleDown(eventData);
        }

        public void HandleUp(PointerEventData eventData)
        {
            m_curHandler.HandleUp(eventData);
        }

        public void HandleClick(PointerEventData eventData)
        {
            m_curHandler.HandleClick(eventData);
        }

        public void HandleEnter(PointerEventData eventData)
        {
            m_curHandler.HandleEnter(eventData);
        }

        public void HandleExit(PointerEventData eventData)
        {
            m_curHandler.HandleExit(eventData);
        }

        public void HandleDrag(PointerEventData eventData)
        {
            m_curHandler.HandleDrag(eventData);
        }
    }
}
