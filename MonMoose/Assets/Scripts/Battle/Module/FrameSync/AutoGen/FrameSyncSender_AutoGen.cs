namespace MonMoose.Battle
{
    public partial class FrameSyncSender
    {
        public void SendFrameSyncReady()
        {
            FrameSyncReadyCommand cmd = m_battleInstance.FetchPoolObj<FrameSyncReadyCommand>(this);
            SendCommand(cmd);
        }
        public void SendMoveToGrid(int entityId, int gridX, int gridY)
        {
            MoveToGridCommand cmd = m_battleInstance.FetchPoolObj<MoveToGridCommand>(this);
            cmd.entityId = entityId;
            cmd.gridX = gridX;
            cmd.gridY = gridY;
            SendCommand(cmd);
        }
        public void SendStagePrepare()
        {
            StagePrepareCommand cmd = m_battleInstance.FetchPoolObj<StagePrepareCommand>(this);
            SendCommand(cmd);
        }
        public void SendStageStart()
        {
            StageStartCommand cmd = m_battleInstance.FetchPoolObj<StageStartCommand>(this);
            SendCommand(cmd);
        }
        public void SendStopMove()
        {
            StopMoveCommand cmd = m_battleInstance.FetchPoolObj<StopMoveCommand>(this);
            SendCommand(cmd);
        }
    }
}
