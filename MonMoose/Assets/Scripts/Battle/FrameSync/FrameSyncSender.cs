using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public class FrameSyncSender
    {
        //private BattleBase m_battleInstance;
        //private IFrameSyncSenderHandler m_handler;

        //public void Init(BattleBase battleInstance)
        //{
        //    m_battleInstance = battleInstance;
        //}

        //public void SendFrameSyncStart()
        //{
        //    FrameSyncStartCommand cmd = m_battleInstance.FetchPoolObj<FrameSyncStartCommand>(this);
        //    SendCommand(cmd);
        //}

        //public void SendStagePrepare()
        //{
        //    StagePrepareCommand cmd = m_battleInstance.FetchPoolObj<StagePrepareCommand>(this);
        //    SendCommand(cmd);
        //}

        //public void SendMoveToGrid(int entityId, GridPosition gridPos)
        //{
        //    MoveToGridCommand cmd = m_battleInstance.FetchPoolObj<MoveToGridCommand>(this);
        //    cmd.entityId = entityId;
        //    cmd.gridPos = gridPos;
        //    SendCommand(cmd);
        //}

        //public void RegisterHandler(IFrameSyncSenderHandler handler)
        //{
        //    m_handler = handler;
        //}

        //private void SendCommand(FrameCommand cmd)
        //{
        //    m_handler.Send(cmd);
        //}
    }
}
