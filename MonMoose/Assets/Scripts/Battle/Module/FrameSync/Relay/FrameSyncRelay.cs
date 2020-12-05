using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public class FrameSyncRelay
    {
        protected FrameSyncSender m_sender;
        protected FrameSyncReceiver m_receiver;

        public void Init(FrameSyncSender sender, FrameSyncReceiver receiver)
        {
            m_sender = sender;
            m_receiver = receiver;
        }

        public virtual void Send(FrameCut cut)
        {
        }

        public virtual void Send(FrameCommandWrap cmdWrap)
        {
        }

        public virtual void Receive(FrameCut cut)
        {
            m_receiver.Receive(cut);
        }

        public virtual void Receive(FrameCommandWrap cmdWrap)
        {
            m_receiver.Receive(cmdWrap);
        }
    }
}
