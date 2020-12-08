namespace MonMoose.Battle
{
    public static class FrameCommandFactory
    {
        public static FrameCommand CreateCommand(BattleBase battleInstance, EFrameCommandType cmdType)
        {
            switch (cmdType)
            {
                case EFrameCommandType.FrameSyncReady:
                    return battleInstance.FetchPoolObj<FrameSyncReadyCommand>(typeof(FrameCommandFactory));
                case EFrameCommandType.MoveToGrid:
                    return battleInstance.FetchPoolObj<MoveToGridCommand>(typeof(FrameCommandFactory));
                case EFrameCommandType.StagePrepare:
                    return battleInstance.FetchPoolObj<StagePrepareCommand>(typeof(FrameCommandFactory));
                case EFrameCommandType.StageStart:
                    return battleInstance.FetchPoolObj<StageStartCommand>(typeof(FrameCommandFactory));
                case EFrameCommandType.StopMove:
                    return battleInstance.FetchPoolObj<StopMoveCommand>(typeof(FrameCommandFactory));
            }
            return null;
        }
    }
}
