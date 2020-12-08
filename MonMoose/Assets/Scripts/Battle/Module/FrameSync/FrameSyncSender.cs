using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public partial class FrameSyncSender
    {
        private FrameSyncRelay m_relay;
        private BattleBase m_battleInstance;

        public void Init(BattleBase battleInstance, FrameSyncRelay relay)
        {
            m_battleInstance = battleInstance;
            m_relay = relay;
        }

        private void SendCommand(FrameCommand cmd)
        {
            FrameCommandWrap wrap = m_battleInstance.FetchPoolObj<FrameCommandWrap>(this);
            wrap.cmd = cmd;
            wrap.playerId = 1;
            m_relay.Send(wrap);
        }
    }
}
