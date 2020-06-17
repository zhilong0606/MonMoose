using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public abstract class StateContext : ClassPoolObj
    {
        public virtual int id
        {
            get { return 0; }
        }
    }
}
