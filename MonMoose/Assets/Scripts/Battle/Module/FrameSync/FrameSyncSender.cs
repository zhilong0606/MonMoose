using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public class FrameSyncSender
    {
        private FrameSyncRelay m_relay;
        private BattleBase m_battleInstance;

        public void Init(BattleBase battleInstance, FrameSyncRelay relay)
        {
            m_battleInstance = battleInstance;
            m_relay = relay;
        }

        public void SendStagePrepare()
        {
            StagePrepareCommand cmd = m_battleInstance.FetchPoolObj<StagePrepareCommand>(this);
            SendCommand(cmd);
        }

        public void SendMoveToGrid(int entityId, GridPosition gridPos)
        {
            MoveToGridCommand cmd = m_battleInstance.FetchPoolObj<MoveToGridCommand>(this);
            cmd.entityId = entityId;
            //cmd.gridPos = gridPos;
            SendCommand(cmd);
        }

        private void SendCommand(FrameCommand cmd)
        {
            m_relay.Send(cmd);
        }
    }
}
