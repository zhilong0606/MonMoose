using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public static class FrameSyncFactory
    {
        public static FrameCommand CreateFrameCommand(BattleBase battleInstance, EFrameCommandType cmdType)
        {
            switch (cmdType)
            {
                case EFrameCommandType.MoveToGrid:
                    return battleInstance.FetchPoolObj<MoveToGridCommand>(typeof(FrameSyncFactory));
            }
            return null;
        }
    }
}
