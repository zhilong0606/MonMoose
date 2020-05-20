using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Core
{
    public abstract class UIProcessComponent : UIComponent, IUIProcess
    {
        private Action<IUIProcess> m_actionOnEnd;

        public virtual bool needSkip
        {
            get { return false; }
        }

        public abstract void StartProcess();

        public void End()
        {
            //CSharpUtility.InvokeSafely(ref m_actionOnEnd, this);
        }

        public void SetActionOnEnd(Action<IUIProcess> actionOnEnd)
        {
            m_actionOnEnd = actionOnEnd;
        }
    }
}
