using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Battle
{
    public class EntityComponentInitInvoker : PriorityInvoker<Action<EntityInitData>>
    {
        private EntityInitData m_initData;

        public void Invoke(EntityInitData initData)
        {
            m_initData = initData;
            Invoke(OnActionInvoke);
            m_initData = null;
        }

        private void OnActionInvoke(Action<EntityInitData> action)
        {
            if (action != null)
            {
                action(m_initData);
            }
        }
    }
}
