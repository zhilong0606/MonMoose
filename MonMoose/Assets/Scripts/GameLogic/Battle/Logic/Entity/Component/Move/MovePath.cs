using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class MovePath : PoolObj
    {
        public MovePathNode tailNode;
        public MovePath nextPath;
        public Grid grid;

        public float g;
        public float h;
        public float f;
    }
}
