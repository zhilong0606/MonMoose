
public class Skill
{
    public SkillInfo info;
    private float coolTimeLeft;
    private bool isCoolDown = true;
    private Schedule schedule;
    //private ScheduleContext m_actionContext = new ScheduleContext();

    public Skill(int skillID)
    {
        info = GameDataManager.instance.skillInfoDic[skillID];
        schedule = ScheduleManager.instance.Load(info.actionPath);
    }

    public void Cast(SkillUseContext context)
    {
        isCoolDown = false;
        //m_actionContext.apointType = info.appointType;
        //m_actionContext.sourceActor = PlayerManager.instance.HostPlayer.selectedActor;
        schedule.Start(context);
    }

    public void UpdateLogic()
    {
        coolTimeLeft -= FrameSyncDefine.DeltaTime;
        if (coolTimeLeft <= 0)
        {
            isCoolDown = true;
        }
        if (schedule.IsStart)
        {
            schedule.Execute();
        }
    }
}
