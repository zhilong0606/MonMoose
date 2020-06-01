using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class SkillComponent : EntityComponent
    {
        private Actor actor;

        public SkillSlot[] skillSlots = new SkillSlot[(int)ESkillSlotType.Count];
        private List<Skill> skillList = new List<Skill>();

        public override EEntityComponentType type
        {
            get { return EEntityComponentType.Skill; }
        }

        public void Init(Actor actor)
        {
            this.actor = actor;
            //int[] skillIds = actor.actorInfo.skillIds;
            //for (int i = 0; i < skillIds.Length; ++i)
            //{
            //    SkillSlot slot = new SkillSlot();
            //    slot.slotType = (ESkillSlotType)i;
            //    Skill skill = new Skill(skillIds[i]);
            //    slot.Init(this.actor, skill);
            //    skillSlots[i] = slot;
            //    skillList.Add(skill);
            //}
        }

        public void FrameUpdate()
        {
            for (int i = 0; i < skillList.Count; ++i)
            {
                skillList[i].UpdateLogic();
            }
        }
    }
}
