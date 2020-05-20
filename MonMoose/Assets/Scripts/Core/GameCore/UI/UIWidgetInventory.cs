using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Core
{
    public class UIWidgetInventory : GameObjectInventory
    {
        public const int defaultId = -1;

        [SerializeField] private int m_id = defaultId;

        private UIComponent m_parent;

        public int Id
        {
            get { return m_id; }
        }

        public UIComponent parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }

        public T GetComponent<T>(int index, bool needInit, object param = null) where T : UIComponent
        {
            GameObject go = Get(index);
            if (go == null)
            {
                return null;
            }
            T component = go.GetComponent<T>();
            if (component != null && needInit)
            {
                component.Initialize(m_parent, param);
            }
            return component;
        }

        public T AddComponent<T>(int index, bool needInit, object param = null) where T : UIComponent
        {
            GameObject go = Get(index);
            if (go == null)
            {
                return null;
            }
            T component = go.AddComponent<T>();
            if (component != null && needInit)
            {
                component.Initialize(m_parent, param);
            }
            return component;
        }
    }
}
