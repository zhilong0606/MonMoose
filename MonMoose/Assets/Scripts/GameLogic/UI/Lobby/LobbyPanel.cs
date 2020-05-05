
using MonMoose.Core;
using UnityEngine;

public class LobbyPanel : UIWindow
{
    protected override void OnInit(object param)
    {
        base.OnInit(param);
        GameObject skillTestBtn = GetWidget((int)EWidget.SkillTestBtn);
        
        UIEventListener.Get(skillTestBtn).SetEvent(UIEventType.PointerClick, LobbyWindow_SkillTestBtn_Click);
    }

    private void LobbyWindow_SkillTestBtn_Click(UIEvent obj)
    {
        EventManager.instance.Broadcast((int)EventID.LobbyWindow_SkillTestBtn_Click);
    }

    private enum EWidget
    {
        SkillTestBtn,
    }
}
