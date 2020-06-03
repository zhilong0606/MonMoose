using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Logic.Battle
{
    public class FrameSyncSender
    {
        private BattleBase m_battleInstance;
        private FrameSyncModule m_frameSyncModule;

        internal void Init(BattleBase battleInstance, FrameSyncModule frameSyncModule)
        {
            m_battleInstance = battleInstance;
            m_frameSyncModule = frameSyncModule;
        }

        public void SendMoveToGrid(int entityId, GridPosition gridPos)
        {
            MoveToGridCommand cmd = m_battleInstance.FetchPoolObj<MoveToGridCommand>();
            cmd.entityId = entityId;
            cmd.gridPos = gridPos;
            SendCommand(cmd);
        }

        private void SendCommand(FrameCommand cmd)
        {
            if (m_frameSyncModule.isLocal)
            {
                m_frameSyncModule.SendDummyServer(cmd);
            }
            else
            {
                byte[] buffer;
                cmd.Serialize(out buffer);
                m_frameSyncModule.SendMsg(buffer);
            }
        }
    }
}
