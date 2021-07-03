using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public class BattleStageStatePrepare : BattleStageState
    {
        public override int stateIndex
        {
            get { return (int)EBattleStageState.Prepare; }
        }

        protected override void OnEnter(StateContext context)
        {
            m_stage.battleInstance.eventListener.StagePrepare();
            base.OnEnter(context);
        }

        protected override void OnExit()
        {
            base.OnExit();
            m_stage.battleInstance.eventListener.FormationEnd();
        }
    }
}
