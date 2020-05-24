using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class ActorInitData
    {
        public int id;

        public static int Sort(ActorInitData x, ActorInitData y)
        {
            return x.id.CompareTo(y.id);
        }
    }
}
