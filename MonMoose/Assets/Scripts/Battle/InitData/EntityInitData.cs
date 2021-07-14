using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class EntityInitData
    {
        public int rid;
        public int level;
        public GridPosition pos;
        public List<SkillInitData> skillList = new List<SkillInitData>();
    }
}
