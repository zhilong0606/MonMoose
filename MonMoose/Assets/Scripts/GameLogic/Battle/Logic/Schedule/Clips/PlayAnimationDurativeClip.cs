namespace MonMoose.Logic.Battle
{
    [ScheduleClip]
    public partial class PlayAnimationDurativeClip : DurativeScheduleClip
    {
        [ScheduleClipMember("AnimName", MotionMemberType.Value)] public string animName;

        public override void OnEnter(SkillUseContext context)
        {
            base.OnEnter(context);
            //context.sourceActor.animationComponent.Play(animName, FrameSyncUtility.MilliToFloat(totalTime));
        }

        public override void OnExit()
        {
            base.OnExit();
            //context.sourceActor.animationComponent.Stop(animName);
        }
    }
}

