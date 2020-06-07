using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonMoose.Core;

namespace MonMoose.Logic.UI
{
    public class BattlePrepareWindow : UIWindow
    {
        protected override void OnInit(object param)
        {
            base.OnInit(param);
            GetInventory().AddComponent<BattlePrepareBottomWidget>((int)EWidget.Bottom, true);
        }

        private enum EWidget
        {
            Bottom,
        }
    }
}
