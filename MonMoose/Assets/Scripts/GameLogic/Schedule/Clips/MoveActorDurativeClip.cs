
[ScheduleClip]
public partial class MoveActorDurativeClip : DurativeScheduleClip
{
    public override void OnEnter(SkillUseContext context)
    {
        base.OnEnter(context);
        context.sourceActor.moveModule.MovePos(context.targetPosition, totalTime);
    }

    public override void OnExit()
    {
        base.OnExit();
        context.sourceActor.moveModule.StopMove();
    }
}
