namespace MonMoose.Battle
{
    public partial class FrameSyncSender
    {
        public void SendFrameSyncReady()
        {
            FrameCommandFrameSyncReady cmd = m_battleInstance.FetchPoolObj<FrameCommandFrameSyncReady>(this);
            SendCommand(cmd);
        }
        public void SendMoveToGrid(int entityId, int gridX, int gridY)
        {
            FrameCommandMoveToGrid cmd = m_battleInstance.FetchPoolObj<FrameCommandMoveToGrid>(this);
            cmd.entityId = entityId;
            cmd.gridX = gridX;
            cmd.gridY = gridY;
            SendCommand(cmd);
        }
        public void SendFormationExchange(int posX1, int posY1, int posX2, int posY2)
        {
            FrameCommandFormationExchange cmd = m_battleInstance.FetchPoolObj<FrameCommandFormationExchange>(this);
            cmd.posX1 = posX1;
            cmd.posY1 = posY1;
            cmd.posX2 = posX2;
            cmd.posY2 = posY2;
            SendCommand(cmd);
        }
        public void SendFormationRetreat(int posX, int posY)
        {
            FrameCommandFormationRetreat cmd = m_battleInstance.FetchPoolObj<FrameCommandFormationRetreat>(this);
            cmd.posX = posX;
            cmd.posY = posY;
            SendCommand(cmd);
        }
        public void SendFormationtEmbattle(int actorId, int posX, int posY)
        {
            FrameCommandFormationtEmbattle cmd = m_battleInstance.FetchPoolObj<FrameCommandFormationtEmbattle>(this);
            cmd.actorId = actorId;
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
    }
}
