using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.Logic
{
    public abstract class InputHandler
    {
        private List<InputTask> m_taskList = new List<InputTask>();

        protected void StartTask(InputTask task)
        {
            m_taskList.Add(task);
            task.actionOnRemove = OnTaskRemove;
            task.Start();
        }

        private void OnTaskRemove(Task task)
        {
            m_taskList.Remove(task as InputTask);
        }

        public void HandleClick(PointerEventData eventData)
        {
            OnHandleClick(eventData);
            for (int i = 0; i < m_taskList.Count; ++i)
            {
                m_taskList[i].HandleClick(eventData);
            }
        }
        public void HandleDown(PointerEventData eventData)
        {
            OnHandleDown(eventData);
            for (int i = 0; i < m_taskList.Count; ++i)
            {
                m_taskList[i].HandleDown(eventData);
            }
        }
        public void HandleUp(PointerEventData eventData)
        {
            OnHandleUp(eventData);
            for (int i = 0; i < m_taskList.Count; ++i)
            {
                m_taskList[i].HandleUp(eventData);
            }
        }
        public void HandleEnter(PointerEventData eventData)
        {
            OnHandleEnter(eventData);
            for (int i = 0; i < m_taskList.Count; ++i)
            {
                m_taskList[i].HandleEnter(eventData);
            }
        }
        public void HandleExit(PointerEventData eventData)
        {
            OnHandleExit(eventData);
            for (int i = 0; i < m_taskList.Count; ++i)
            {
                m_taskList[i].HandleExit(eventData);
            }
        }
        public void HandleDrag(PointerEventData eventData)
        {
            OnHandleExit(eventData);
            for (int i = 0; i < m_taskList.Count; ++i)
            {
                m_taskList[i].HandleDrag(eventData);
            }
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
    }
}
