using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public partial class BattleBase
    {
        public bool FindPath(Entity entity, BattleGrid startGrid, DcmVec2 offset, BattleGrid targetGrid, List<BattleGrid> gridList)
        {
            if (m_pathFindModule != null)
            {
                return m_pathFindModule.FindPath(entity, startGrid, offset, targetGrid, gridList);
            }
            return default;
        }
    }
}
