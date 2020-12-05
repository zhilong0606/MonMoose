namespace MonMoose.Battle
{
    [ScheduleClip]
    public partial class HitActorDurativeClip : DurativeScheduleClip
    {
        [ScheduleClipMember("Radius", MotionMemberType.Value)] public int radius;

        public override void OnEnter(SkillUseContext context)
        {
            base.OnEnter(context);
            //FixVec3 position = context.sourceActor.moveComponent.Position;
            //List<Actor> actorList = ActorManager.instance.actorList;
            //for (int i = 0; i < actorList.Count; ++i)
            //{
            //    if (actorList[i] != context.sourceActor && (actorList[i].moveComponent.Position - position).sqrMagnitude <= radius * radius)
            //    {
            //        actorList[i].attributeComponent.TakeDamage();
            //    }
            //}
        }

        public override void OnExecute()
        {
            base.OnExecute();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
