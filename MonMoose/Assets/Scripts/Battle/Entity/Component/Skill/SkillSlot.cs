namespace MonMoose.Battle
{
    public class SkillSlot
    {
        public ESkillSlotType slotType;
        public Skill curSkill;
        private SkillUseContext skillUseContext = new SkillUseContext();

        public void Init(Actor actor, Skill skill)
        {
            curSkill = skill;
            skillUseContext.sourceActor = actor;
        }

        public void CastSkillTarget(Actor actor)
        {
            skillUseContext.apointType = ESkillAppointType.Target;
            skillUseContext.targetActor = actor;
            curSkill.Cast(skillUseContext);
        }

        //public void CastSkillPosition(FixVec3 position)
        //{
        //    skillUseContext.apointType = ESkillAppointType.Position;
        //    skillUseContext.targetPosition = position;
        //    curSkill.Cast(skillUseContext);
        //}

        //public void CastSkillDirection(FixVec3 direction)
        //{
        //    skillUseContext.apointType = ESkillAppointType.Direction;
        //    skillUseContext.targetDirection = direction;
        //    curSkill.Cast(skillUseContext);
        //}


        public void UpdateLogic(int deltaTime)
        {

        }
    }
}
