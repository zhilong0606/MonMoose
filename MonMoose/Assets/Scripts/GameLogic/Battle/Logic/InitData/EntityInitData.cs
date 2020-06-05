using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class EntityInitData
    {
        public int id;
        public int level;
        public GridPosition pos;
        public List<SkillInitData> skillList = new List<SkillInitData>();
    }
}
