namespace MonMoose.Battle
{
    public partial class FrameSyncSender
    {
        public void SendFrameSyncReady()
        {
            FrameCommandFrameSyncReady cmd = m_battleInstance.FetchPoolObj<FrameCommandFrameSyncReady>(this);
            SendCommand(cmd);
        }
        public void SendMoveToGrid(int entityUid, int gridX, int gridY)
        {
            FrameCommandMoveToGrid cmd = m_battleInstance.FetchPoolObj<FrameCommandMoveToGrid>(this);
            cmd.entityUid = entityUid;
            cmd.gridX = gridX;
            cmd.gridY = gridY;
            SendCommand(cmd);
        }
        public void SendFormationExchange(int entityUid1, int entityUid2)
        {
            FrameCommandFormationExchange cmd = m_battleInstance.FetchPoolObj<FrameCommandFormationExchange>(this);
            cmd.entityUid1 = entityUid1;
            cmd.entityUid2 = entityUid2;
            SendCommand(cmd);
        }
        public void SendFormationRetreat(int entityUid)
        {
            FrameCommandFormationRetreat cmd = m_battleInstance.FetchPoolObj<FrameCommandFormationRetreat>(this);
            cmd.entityUid = entityUid;
            SendCommand(cmd);
        }
        public void SendFormationtEmbattle(int entityRid, int posX, int posY)
        {
            FrameCommandFormationtEmbattle cmd = m_battleInstance.FetchPoolObj<FrameCommandFormationtEmbattle>(this);
            cmd.entityRid = entityRid;
            cmd.posX = posX;
            cmd.posY = posY;
            SendCommand(cmd);
        }
        public void SendFormationEnd()
        {
            FrameCommandFormationEnd cmd = m_battleInstance.FetchPoolObj<FrameCommandFormationEnd>(this);
            SendCommand(cmd);
        }
        public void SendStageStart()
        {
            FrameCommandStageStart cmd = m_battleInstance.FetchPoolObj<FrameCommandStageStart>(this);
            SendCommand(cmd);
        }
        public void SendStagePrepare()
        {
            FrameCommandStagePrepare cmd = m_battleInstance.FetchPoolObj<FrameCommandStagePrepare>(this);
            SendCommand(cmd);
        }
        public void SendStageEnd()
        {
            FrameCommandStageEnd cmd = m_battleInstance.FetchPoolObj<FrameCommandStageEnd>(this);
            SendCommand(cmd);
        }
        public void SendStopMove()
        {
            FrameCommandStopMove cmd = m_battleInstance.FetchPoolObj<FrameCommandStopMove>(this);
            SendCommand(cmd);
        }
        public void SendStepEnd()
        {
            FrameCommandStepEnd cmd = m_battleInstance.FetchPoolObj<FrameCommandStepEnd>(this);
            SendCommand(cmd);
        }
        public void SendActorWait(int entityUid)
        {
            FrameCommandActorWait cmd = m_battleInstance.FetchPoolObj<FrameCommandActorWait>(this);
            cmd.entityUid = entityUid;
            SendCommand(cmd);
        }
    }
}
