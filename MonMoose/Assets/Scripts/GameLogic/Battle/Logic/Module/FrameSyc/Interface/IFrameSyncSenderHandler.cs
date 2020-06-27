using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public interface IFrameSyncSenderHandler
    {
        void Send(FrameCommand cmd);
    }
}
