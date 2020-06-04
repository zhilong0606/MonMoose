using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class MovePathNode : PoolObj
    {
        public Grid grid;
        public MovePathNode nextNode;

        public bool IsSamePosition(MovePathNode node)
        {
            return grid == node.grid;
        }
    }
}
