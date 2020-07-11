using System.Collections;
using System.Collections.Generic;

namespace MonMoose.BattleLogic
{
    public abstract class StateContext : PoolObj
    {
        public virtual int id
        {
            get { return 0; }
        }
    }
}
