namespace MonMoose.Logic.Battle
{
    public class FrameCommand /*: ClassPoolObj*/
    {
        public int playerID;
        public int frameNum;
        public EFrameCommandType commandType;

        public static FrameCommand GetCommand(EFrameCommandType type)
        {
            //switch (type)
            //{
            //    case EFrameCommandType.MoveDirection:
            //        return ClassPoolManager.instance.Fetch<MoveDirectionCommand>();
            //}
            return null;
        }

        public virtual void Serialize(out byte[] buffer)
        {
            buffer = null;
        }

        public virtual void Deserialise(ref byte[] buffer, ref int offset)
        {

        }

        public virtual void Excute()
        {

        }
    }
}
