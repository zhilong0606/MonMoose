using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public abstract class AbstractEventParams : ClassPoolObj
    {
        public abstract void Broadcast();
    }
}
