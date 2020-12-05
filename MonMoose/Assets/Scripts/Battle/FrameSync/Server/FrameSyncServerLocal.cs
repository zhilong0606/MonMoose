using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class FrameSyncServerLocal : FrameSyncServer, IFrameSyncSenderHandler
    {
        public void Send(FrameCommand cmd)
        {
            //Receive(cmd);
        }

        //public override void SendFrameCut(FrameCut cut)
        //{
        //    m_frameSyncModule.ReceiveFrameCut(cut);
        //}
    }
}
