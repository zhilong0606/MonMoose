
using MonMoose.Core;
using UnityEngine;
using UnityEngine.UI;

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
        GameManager.Instance.EnterBattle();
    }

    private enum EWidget
    {
        StartBtn,
    }
}
