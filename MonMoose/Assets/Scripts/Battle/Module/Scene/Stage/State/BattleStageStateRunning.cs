using MonMoose.Core;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class BattleStageStateRunning : BattleStageState
    {
        public override int stateIndex
        {
            get { return (int)EBattleStageState.Running; }
        }

        protected override void OnEnter(StateContext context)
        {
            base.OnEnter(context);
            m_stage.battleInstance.eventListener.MainBattleStart();
        }

        protected override void OnExit()
        {
            base.OnExit();
            m_stage.battleInstance.eventListener.MainBattleEnd();
        }
    }
}
