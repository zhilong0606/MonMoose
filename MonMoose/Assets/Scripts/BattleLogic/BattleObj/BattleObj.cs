﻿using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.BattleLogic
{
    public abstract class BattleObj : ClassPoolObj
    {
        protected BattleBase m_battleInstance;

        public void SetBattleInstance(BattleBase battleInstance)
        {
            m_battleInstance = battleInstance;
        }
    }
}
