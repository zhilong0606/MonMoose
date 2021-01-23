namespace MonMoose.Battle
{
    public partial class FrameCommandFormationRetreat
    {
        public int posX;
        public int posY;

        public override EFrameCommandType commandType
        {
            get { return EFrameCommandType.FormationRetreat; }
        }

        protected override byte GetBitFlagCount()
        {
            return (int)ESerializeIndex.Max;
        }

        protected override bool CheckValid(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PosX:
                    return posX != default(int);
                case ESerializeIndex.PosY:
                    return posY != default(int);
            }
            return false;
        }

        protected override int GetSizeOf(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PosX:
                    return sizeof(int);
                case ESerializeIndex.PosY:
                    return sizeof(int);
            }
            return 0;
        }

        protected override void SerializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PosX:
                    ByteBufferUtility.WriteInt(buffer, ref offset, posX);
                    break;
                case ESerializeIndex.PosY:
                    ByteBufferUtility.WriteInt(buffer, ref offset, posY);
                    break;
            }
        }

        protected override void DeserializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PosX:
                    posX = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
                case ESerializeIndex.PosY:
                    posY = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
            }
        }

        private enum ESerializeIndex
        {
            PosX,
            PosY,

            Max
        }
    }
}
