using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public partial class BattleBase
    {
        public int CreateObjId(EBattleObjType type)
        {
            if (m_objIdModule != null)
            {
                return m_objIdModule.CreateObjId(type);
            }
            return default;
        }
    }
}
