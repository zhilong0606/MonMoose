
using MonMoose.Core;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : UIWindow
{
    protected override void OnInit(object param)
    {
        base.OnInit(param);
        Button startBtn = GetComponentToWidget<Button>((int)EWidget.StartBtn);

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
