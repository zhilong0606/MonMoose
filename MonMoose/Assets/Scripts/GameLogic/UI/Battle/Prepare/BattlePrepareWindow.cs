using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonMoose.Core;

namespace MonMoose.GameLogic.UI
{
    public class BattlePrepareWindow : UIWindow
    {
        protected override void OnInit(object param)
        {
            base.OnInit(param);
            GetInventory().AddComponent<BattlePrepareBottomWidget>((int)EWidget.Bottom, true);
        }

        protected override void RegisterListener()
        {
            base.RegisterListener();
            EventManager.instance.RegisterListener((int)EventID.BattlePrepare_Finish, OnPreareFinished);
        }

        protected override void UnRegisterListener()
        {
            base.UnRegisterListener();
            EventManager.instance.UnRegisterListener((int)EventID.BattlePrepare_Finish, OnPreareFinished);
        }

        private void OnPreareFinished()
        {
            SetActive(false);
        }

        private enum EWidget
        {
            Bottom,
        }
    }
}
