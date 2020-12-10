using MonMoose.Core;

namespace MonMoose.Battle
{
    public abstract class FrameCommand : FrameMsgObj
    {
        public override bool isBitFlagConst
        {
            get { return true; }
        }

        public abstract EFrameCommandType commandType { get; }

        public abstract bool Execute(int playerId);
    }
}
