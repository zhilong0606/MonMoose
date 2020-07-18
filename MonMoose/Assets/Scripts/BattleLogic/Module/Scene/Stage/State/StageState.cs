using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.BattleLogic
{
    public abstract class StageState : State
    {
        protected Stage m_stage;
        protected BattleBase m_battleInstance;

        public void Init(Stage stage, BattleBase battleInstance)
        {
            m_stage = stage;
            m_battleInstance = battleInstance;
        }
    }
}
