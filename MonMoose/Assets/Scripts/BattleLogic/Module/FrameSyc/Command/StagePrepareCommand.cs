using System.Collections;
using System.Collections.Generic;

namespace MonMoose.BattleLogic
{
    public class StagePrepareCommand : FrameCommand
    {
        public override EFrameCommandType commandType
        {
            get { return EFrameCommandType.StagePrepare; }
        }

        public override void Excute(int playerId)
        {
            m_battleInstance.WaitFrameCommand(EFrameCommandType.StageStart);
        }
    }
}
