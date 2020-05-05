using UnityEngine;

public class JoystickPanel : UIWindow
{
    protected override void OnInit(object param)
    {
        base.OnInit(param);
        GameObjectInventory inventory = GetComponent<GameObjectInventory>();
        Joystick moveJoystick = inventory.GetComponent<Joystick>((int)EWidget.MoveJoystick);
        moveJoystick.Initialize(window, (int)EJoystickType.Move);
        Joystick skillJoystick = inventory.GetComponent<Joystick>((int)EWidget.SkillJoystick);
        skillJoystick.Initialize(window, (int)EJoystickType.Skill);
        GameObject moveTouchArea = inventory.Get((int)EWidget.MoveTouchArea);
        GameObject skillBtn1 = inventory.Get((int)EWidget.SkillButton1);
        GameObject skillBtn2 = inventory.Get((int)EWidget.SkillButton2);
        GameObject skillBtn3 = inventory.Get((int)EWidget.SkillButton3);
        GameObject skillBtn4 = inventory.Get((int)EWidget.SkillButton4);
        moveJoystick.RegisterTrigger(moveTouchArea);
        skillJoystick.RegisterTrigger(skillBtn1, (int)ESkillSlotType.NormalAttack);
        skillJoystick.RegisterTrigger(skillBtn2, (int)ESkillSlotType.SkillSlot1);
        skillJoystick.RegisterTrigger(skillBtn3, (int)ESkillSlotType.SkillSlot2);
        skillJoystick.RegisterTrigger(skillBtn4, (int)ESkillSlotType.SkillSlot3);
    }

    private enum EWidget
    {
        MoveJoystick,
        SkillJoystick,
        MoveTouchArea,
        SkillButton1,
        SkillButton2,
        SkillButton3,
        SkillButton4,
    }
}
