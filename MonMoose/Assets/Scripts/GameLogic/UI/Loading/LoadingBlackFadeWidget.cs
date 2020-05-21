using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Logic
{
    public class LoadingBlackFadeWidget : UIComponent
    {
        private AnimationController m_animCtrl;
        private Action m_actionOnFadeInEnd;

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            m_animCtrl = GetComponent<AnimationController>();
            m_animCtrl.Init();
        }

        public void FadeIn(Action actionOnEnd)
        {
            m_actionOnFadeInEnd = actionOnEnd;
            m_animCtrl.Play((int)EAnim.FadeIn, OnFadeInEnd);
        }

        private void OnFadeInEnd()
        {
            if (m_actionOnFadeInEnd != null)
            {
                Action temp = m_actionOnFadeInEnd;
                m_actionOnFadeInEnd = null;
                temp();
            }
        }

        public void FadeOut(Action actionOnEnd)
        {
            m_animCtrl.Play((int)EAnim.FadeOut, actionOnEnd);
        }

        private enum EAnim
        {
            FadeIn,
            FadeOut,
        }
    }
}
