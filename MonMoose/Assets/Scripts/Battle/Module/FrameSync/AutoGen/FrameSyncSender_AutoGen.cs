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
        public void SendStagePrepare()
        {
            FrameCommandStagePrepare cmd = m_battleInstance.FetchPoolObj<FrameCommandStagePrepare>(this);
            SendCommand(cmd);
        }
        public void SendStageStart()
        {
            FrameCommandStageStart cmd = m_battleInstance.FetchPoolObj<FrameCommandStageStart>(this);
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
