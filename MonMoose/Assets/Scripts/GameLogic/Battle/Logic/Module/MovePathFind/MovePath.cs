using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class MovePath : PoolObj
    {
        public MovePath fromPath;
        public Grid grid;
        public FixVec2 offset;

        public Fix32 g;
        public Fix32 h;
        public Fix32 f;

        public override void OnRelease()
        {
            base.OnRelease();
            fromPath = null;
            grid = null;
            offset = FixVec2.zero;
            g = Fix32.zero;
            h = Fix32.zero;
            f = Fix32.zero;
        }
    }
}
