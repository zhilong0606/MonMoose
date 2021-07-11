using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonMoose.Core;
using UnityEngine.UI;

namespace MonMoose.GameLogic.UI
{
    public class BattleMainNotifyWidget : UIComponent
    {
        private int m_timerId;

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            SetActive(false);
        }

        protected override void OnUnInit()
        {
            base.OnUnInit();
            TimerManager.instance.Stop(ref m_timerId);
        }

        protected override void RegisterListener()
        {
            base.RegisterListener();
            EventManager.instance.RegisterListener((int)EventID.BattleStart, OnBattleStart);
        }

        protected override void UnRegisterListener()
        {
            base.UnRegisterListener();
            EventManager.instance.UnRegisterListener((int)EventID.BattleStart, OnBattleStart);
        }

        private void OnBattleStart()
        {
            ShowState(EState.BattleStart);
        }

        private void OnTimeUp()
        {
            gameObject.SetActive(false);
        }

        public void ShowState(EState state)
        {
            gameObject.SetActive(true);
            m_timerId = TimerManager.instance.Start(1f, OnTimeUp);
        }

        public enum EState
        {
            BattleStart,
        }
    }
}
