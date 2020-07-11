using System.Collections;
using System.Collections.Generic;

namespace MonMoose.BattleLogic
{
    public class FrameSyncStartCommand : FrameCommand
    {
        public override EFrameCommandType commandType
        {
            get { return EFrameCommandType.FrameStart; }
        }

        public override void Excute(int playerId)
        {
            base.Excute(playerId);
            m_battleInstance.StartFrameSync();
        }
    }
}
