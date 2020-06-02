using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public abstract class BattleObj : PoolObj
    {
        protected BattleBase m_battleInstance;

        public void SetBattleInstance(BattleBase battleInstance)
        {
            m_battleInstance = battleInstance;
        }
    }
}
