using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public abstract class BattleStageState : State
    {
        protected BattleStage m_stage;

        public void Init(BattleStage stage)
        {
            m_stage = stage;
        }
    }
}
