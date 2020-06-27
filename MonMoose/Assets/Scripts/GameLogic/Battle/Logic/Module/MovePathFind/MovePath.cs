using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class MovePath : PoolObj
    {
        public MovePath fromPath;
        public Grid grid;
        public DcmVec2 offset;

        public Dcm32 g;
        public Dcm32 h;
        public Dcm32 f;

        public override void OnRelease()
        {
            base.OnRelease();
            fromPath = null;
            grid = null;
            offset = DcmVec2.zero;
            g = Dcm32.zero;
            h = Dcm32.zero;
            f = Dcm32.zero;
        }
    }
}
