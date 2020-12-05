using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Logic.UI
{
    public class LoadingWindow : UIWindow
    {
        public static void OpenLoading(ELoadingId id, ELoadingWindowType windowType, Action actionOnEnd = null)
        {
            LoadingWindowContext ctx = ClassPoolManager.instance.Fetch<LoadingWindowContext>(typeof(LoadingWindow));
            ctx.id = id;
            ctx.windowType = windowType;
            ctx.actionOnEnd = actionOnEnd;
            UIWindowManager.instance.OpenWindow((int)EWindowId.Loading, ctx);
        }

        public static void CloseLoading(ELoadingId id, Action actionOnEnd = null)
        {
            LoadingWindow loadingWindow = UIWindowManager.instance.GetWindow<LoadingWindow>((int)EWindowId.Loading);
            if (loadingWindow != null)
            {
                LoadingWindowContext ctx = ClassPoolManager.instance.Fetch<LoadingWindowContext>(typeof(LoadingWindow));
                ctx.id = id;
                ctx.actionOnEnd = actionOnEnd;
                loadingWindow.Hide(ctx);
            }
        }

        private LoadingFadeBlackWidget m_fadeWidget;
        private bool m_isOpened;
        private ELoadingWindowType m_openedWindowType;

        private List<LoadingWindowContext> m_openCtxList = new List<LoadingWindowContext>();
        private List<LoadingWindowContext> m_closeCtxList = new List<LoadingWindowContext>();
        private List<LoadingWindowContext> m_tempCtxList = new List<LoadingWindowContext>();

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            m_fadeWidget = GetInventory().AddComponent<LoadingFadeBlackWidget>((int)EWidget.BlackFade, true);
        }

        public override void OnOpened(object param)
        {
            base.OnOpened(param);
            LoadingWindowContext openCtx = param as LoadingWindowContext;
            AddOpenCtx(openCtx);
        }

        private void Hide(LoadingWindowContext closeCtx)
        {
            AddCloseCtx(closeCtx);
        }

        private void OnFadeBlackIn()
        {
            m_isOpened = true;
            for (int i = 0; i < m_openCtxList.Count; ++i)
            {
                if (m_openCtxList[i].actionOnEnd != null)
                {
                    m_openCtxList[i].actionOnEnd();
                }
            }
            m_tempCtxList.AddRange(m_closeCtxList);
            for (int i = m_tempCtxList.Count - 1; i >= 0; --i)
            {
                LoadingWindowContext closeCtx = m_tempCtxList[i];
                LoadingWindowContext openCtx = FindCtxFromList(closeCtx.id, m_openCtxList);
                RemoveOpenCtx(openCtx);
            }
        }

        private LoadingWindowContext FindCtxFromList(ELoadingId id, List<LoadingWindowContext> ctxList)
        {
            for (int i = 0; i < ctxList.Count; ++i)
            {
                if (ctxList[i].id == id)
                {
                    return ctxList[i];
                }
            }
            return null;
        }


        private void AddOpenCtx(LoadingWindowContext openCtx)
        {
            if (!m_isOpened)
            {
                if (m_openCtxList.Count == 0)
                {
                    m_openedWindowType = openCtx.windowType;
                    if (openCtx.windowType == ELoadingWindowType.FadeBlack)
                    {
                        m_fadeWidget.SetActive(true);
                        m_fadeWidget.FadeIn(OnFadeBlackIn);
                    }
                }
            }
            else
            {
                if (openCtx.actionOnEnd != null)
                {
                    openCtx.actionOnEnd();
                }
            }
            m_openCtxList.Add(openCtx);
        }

        private void AddCloseCtx(LoadingWindowContext closeCtx)
        {
            m_closeCtxList.Add(closeCtx);
            if (m_isOpened)
            {
                LoadingWindowContext openCtx = FindCtxFromList(closeCtx.id, m_openCtxList);
                if (openCtx != null)
                {
                    RemoveOpenCtx(openCtx);
                }
            }
        }

        private void RemoveOpenCtx(LoadingWindowContext openCtx)
        {
            openCtx.Release();
            m_openCtxList.Remove(openCtx);
            if (m_openCtxList.Count == 0)
            {
                if (m_openedWindowType == ELoadingWindowType.FadeBlack)
                {
                    m_fadeWidget.FadeOut(OnHideEnd);
                }
            }
        }

        private void OnHideEnd()
        {
            for (int i = 0; i < m_closeCtxList.Count; ++i)
            {
                if (m_closeCtxList[i].actionOnEnd != null)
                {
                    m_closeCtxList[i].actionOnEnd();
                }
                m_closeCtxList[i].Release();
            }
            m_closeCtxList.Clear();
            m_tempCtxList.Clear();
            m_fadeWidget.SetActive(false);
            SetActive(false);
        }

        private enum EWidget
        {
            BlackFade,
        }
    }
}
