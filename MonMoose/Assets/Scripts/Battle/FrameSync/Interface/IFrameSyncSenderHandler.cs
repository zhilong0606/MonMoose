using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public interface IFrameSyncSenderHandler
    {
        void Send(FrameCommand cmd);
    }
}
