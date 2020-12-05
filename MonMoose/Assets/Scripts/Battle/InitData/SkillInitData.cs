using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class SkillInitData
    {
        public int id;
        public int level;

        public static int Sort(SkillInitData x, SkillInitData y)
        {
            return x.id.CompareTo(y.id);
        }
    }
}
