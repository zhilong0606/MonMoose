namespace MonMoose.Battle
{
    public static class FrameCommandFactory
    {
        public static FrameCommand CreateCommand(BattleBase battleInstance, EFrameCommandType cmdType)
        {
            switch (cmdType)
            {
                case EFrameCommandType.FrameSyncReady:
                    return battleInstance.FetchPoolObj<FrameCommandFrameSyncReady>(typeof(FrameCommandFactory));
                case EFrameCommandType.MoveToGrid:
                    return battleInstance.FetchPoolObj<FrameCommandMoveToGrid>(typeof(FrameCommandFactory));
                case EFrameCommandType.FormationExchange:
                    return battleInstance.FetchPoolObj<FrameCommandFormationExchange>(typeof(FrameCommandFactory));
                case EFrameCommandType.FormationRetreat:
                    return battleInstance.FetchPoolObj<FrameCommandFormationRetreat>(typeof(FrameCommandFactory));
                case EFrameCommandType.FormationtEmbattle:
                    return battleInstance.FetchPoolObj<FrameCommandFormationtEmbattle>(typeof(FrameCommandFactory));
                case EFrameCommandType.FormationEnd:
                    return battleInstance.FetchPoolObj<FrameCommandFormationEnd>(typeof(FrameCommandFactory));
                case EFrameCommandType.StageStart:
                    return battleInstance.FetchPoolObj<FrameCommandStageStart>(typeof(FrameCommandFactory));
                case EFrameCommandType.StagePrepare:
                    return battleInstance.FetchPoolObj<FrameCommandStagePrepare>(typeof(FrameCommandFactory));
                case EFrameCommandType.StageEnd:
                    return battleInstance.FetchPoolObj<FrameCommandStageEnd>(typeof(FrameCommandFactory));
                case EFrameCommandType.StopMove:
                    return battleInstance.FetchPoolObj<FrameCommandStopMove>(typeof(FrameCommandFactory));
                case EFrameCommandType.StepEnd:
                    return battleInstance.FetchPoolObj<FrameCommandStepEnd>(typeof(FrameCommandFactory));
                case EFrameCommandType.ActorWait:
                    return battleInstance.FetchPoolObj<FrameCommandActorWait>(typeof(FrameCommandFactory));
            }
            return null;
        }
    }
}
