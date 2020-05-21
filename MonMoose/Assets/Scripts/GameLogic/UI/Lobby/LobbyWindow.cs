using MonMoose.Core;
using UnityEngine.UI;

namespace MonMoose.Logic
{

    public class LobbyWindow : UIWindow
    {
        protected override void OnInit(object param)
        {
            base.OnInit(param);
            Button startBtn = GetInventory().GetComponent<Button>((int)EWidget.StartBtn);

            startBtn.onClick.AddListener(OnStartBtnClick);
        }

        private void OnStartBtnClick()
        {
            GameManager.instance.EnterBattle();
        }

        private enum EWidget
        {
            StartBtn,
        }
    }
}
