using System.Collections;
using System.Collections.Generic;

namespace MonMoose.BattleLogic
{
    public interface IFrameSyncSenderHandler
    {
        void Send(FrameCommand cmd);
    }
}
