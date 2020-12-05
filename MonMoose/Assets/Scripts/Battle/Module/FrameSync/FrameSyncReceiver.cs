using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
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
            cut.Execute();
        }

        public void Receive(FrameCommandWrap cmdWrap)
        {
            m_handler.AddCommand(cmdWrap.playerId, cmdWrap.cmd);
            cmdWrap.Release();
        }
    }
}
