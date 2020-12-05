using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class StageStartCommand : FrameCommand
    {
        public override EFrameCommandType commandType
        {
            get { return EFrameCommandType.StageStart; }
        }
    }
}
