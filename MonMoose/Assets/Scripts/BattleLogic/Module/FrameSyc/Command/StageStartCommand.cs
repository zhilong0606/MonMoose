using System.Collections;
using System.Collections.Generic;

namespace MonMoose.BattleLogic
{
    public class StageStartCommand : FrameCommand
    {
        public override EFrameCommandType commandType
        {
            get { return EFrameCommandType.StageStart; }
        }
    }
}
