using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Logic.UI
{
    public class LoadingWindow : UIWindow
    {
        private LoadingBlackFadeWidget m_fadeWidget;

        private Action m_actionOnShowEnd;

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            m_fadeWidget = GetInventory().AddComponent<LoadingBlackFadeWidget>((int)EWidget.BlackFade, true);
        }

        protected override void RegisterListener()
        {
            EventManager.instance.RegisterListener<Action>((int)EventID.LoadingWindow_FadeInRequest, OnFadeInRequest);
            EventManager.instance.RegisterListener((int)EventID.LoadingWindow_FadeOutRequest, OnFadeOutRequest);
        }

        protected override void UnregisterListener()
        {
            EventManager.instance.UnregisterListener<Action>((int)EventID.LoadingWindow_FadeInRequest, OnFadeInRequest);
            EventManager.instance.UnregisterListener((int)EventID.LoadingWindow_FadeOutRequest, OnFadeOutRequest);
        }

        private void OnFadeInRequest(Action actionOnShowEnd)
        {
            m_fadeWidget.SetActive(true);
            m_fadeWidget.FadeIn(actionOnShowEnd);
        }

        private void OnFadeOutRequest()
        {
            if (m_actionOnShowEnd != null)
            {
                Action temp = m_actionOnShowEnd;
                m_actionOnShowEnd = null;
                temp();
            }
            m_fadeWidget.FadeOut(OnHideEnd);
        }

        private void OnHideEnd()
        {
            m_fadeWidget.SetActive(false);
        }

        private enum EWidget
        {
            BlackFade,
        }
    }
}
