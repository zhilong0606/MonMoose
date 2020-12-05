using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public class FrameSyncReceiver
    {
        private FrameSyncRelay m_relay;
        private FrameSyncHandler m_handler;

        public void Init(FrameSyncHandler handler, FrameSyncRelay relay)
        {
            m_relay = relay;
            m_handler = handler;
        }

        public void Receive(FrameCut cut)
        {
            //m_handler.add
            cut.Excute();
        }

        public void Receive(FrameCommand cmd)
        {
            //m_handler.AddFrameCommand(cmd);
        }
    }
}
