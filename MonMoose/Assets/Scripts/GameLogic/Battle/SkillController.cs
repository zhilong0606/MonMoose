using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

public class SkillController
{
    public SkillIndicator curIndicator;

    private Dictionary<string, SkillIndicator> indicatorDic = new Dictionary<string, SkillIndicator>();
    private SkillIndicatorContext indicatorContext = new SkillIndicatorContext();

    public void Init()
    {
        Actor actor = PlayerManager.instance.HostPlayer.selectedActor;
        for (int i = 0; i < actor.skillModule.skillSlots.Length; ++i)
        {
            if (actor.skillModule.skillSlots[i] == null)
            {
                continue;
            }
            SkillInfo info = actor.skillModule.skillSlots[i].curSkill.info;
            if (!indicatorDic.ContainsKey(info.indicatorPath))
            {
                GameObject prefab = ResourceManager.instance.GetResource(info.indicatorPath) as GameObject;
                SkillIndicator indicator = Object.Instantiate(prefab).GetComponent<SkillIndicator>();
                indicator.Init(actor.gameObject, indicatorContext);
                indicatorDic.Add(info.indicatorPath, indicator);
            }
        }
        RegisterListener();
    }

    private void RegisterListener()
    {
        JoystickManager.instance.RegisterActionDragUpdate((int)EJoystickType.Skill, OnSkillDragUpdate);
        JoystickManager.instance.RegisterActionStateChanged((int)EJoystickType.Skill, OnSkillStateChanged);
        JoystickManager.instance.RegisterActionValidChanged((int)EJoystickType.Skill, OnSkillValidChanged);
    }

    private void RemoveListener()
    {
        JoystickManager.instance.UnregisterActionDragUpdate((int)EJoystickType.Skill, OnSkillDragUpdate);
        JoystickManager.instance.UnregisterActionStateChanged((int)EJoystickType.Skill, OnSkillStateChanged);
        JoystickManager.instance.UnregisterActionValidChanged((int)EJoystickType.Skill, OnSkillValidChanged);
    }

    private void OnSkillDragUpdate(JoystickEvent e)
    {
        Skill curSkill = PlayerManager.instance.HostPlayer.selectedActor.skillModule.skillSlots[e.triggerId].curSkill;
        Vector3 pos = new Vector3(e.normal.x, 0, e.normal.y);
        Vector3 normal = Camera.main.transform.TransformDirection(pos);
        normal.y = 0;
        if (normal != Vector3.zero)
        {
            indicatorContext.direction = normal;
            indicatorContext.distance = e.rate * FrameSyncUtility.MilliToFloat(curSkill.info.attackRange);
        }
    }

    private void OnSkillStateChanged(JoystickEvent e)
    {
        Player hostPlayer = PlayerManager.instance.HostPlayer;
        Actor hostActor = hostPlayer.selectedActor;
        if (e.state == EInputState.Down)
        {
            Skill curSkill = hostActor.skillModule.skillSlots[e.triggerId].curSkill;
            curIndicator = indicatorDic[curSkill.info.indicatorPath];
            curIndicator.SetSkill(curSkill);
            curIndicator.Visible = true;
        }
        else
        {
            curIndicator.Visible = false;
            curIndicator = null;
            ESkillAppointType appointType = hostActor.skillModule.skillSlots[e.triggerId].curSkill.info.appointType;
            ESkillSlotType slotType = (ESkillSlotType)e.triggerId;
            switch (appointType)
            {
                case ESkillAppointType.Position:
                {
                    CastSkillPositionCommand command = ClassPoolManager.instance.Fetch<CastSkillPositionCommand>();
                    command.playerID = hostPlayer.playerID;
                    command.slotType = (short)slotType;
                    command.position = indicatorContext.direction * indicatorContext.distance + hostActor.transform.position;
                    FrameSyncManager.instance.Send(command);
                    break;
                }
                case ESkillAppointType.Direction:
                {
                    CastSkillDirectionCommand command = ClassPoolManager.instance.Fetch<CastSkillDirectionCommand>();
                    command.playerID = hostPlayer.playerID;
                    command.slotType = (short)slotType;
                    //command.normal = indicatorContext.normal.ToIntVector3();
                    FrameSyncManager.instance.Send(command);
                    break;
                }
            }
        }
    }

    private void OnSkillValidChanged(JoystickEvent e)
    {

    }

    public void Clear()
    {
        RemoveListener();
    }
}
