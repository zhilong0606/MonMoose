using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class BattleObj : PoolObj
    {
        protected BattleBase m_battleInstance;

        protected void GetInsFrom(BattleObj obj)
        {
            m_battleInstance = obj.m_battleInstance;
        }
    }
}
