namespace MonMoose.Battle
{
    public partial class FrameCommandFormationExchange
    {
        public int posX1;
        public int posY1;
        public int posX2;
        public int posY2;

        public override EFrameCommandType commandType
        {
            get { return EFrameCommandType.FormationExchange; }
        }

        protected override byte GetBitFlagCount()
        {
            return (int)ESerializeIndex.Max;
        }

        protected override bool CheckValid(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PosX1:
                    return posX1 != default(int);
                case ESerializeIndex.PosY1:
                    return posY1 != default(int);
                case ESerializeIndex.PosX2:
                    return posX2 != default(int);
                case ESerializeIndex.PosY2:
                    return posY2 != default(int);
            }
            return false;
        }

        protected override int GetSizeOf(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PosX1:
                    return sizeof(int);
                case ESerializeIndex.PosY1:
                    return sizeof(int);
                case ESerializeIndex.PosX2:
                    return sizeof(int);
                case ESerializeIndex.PosY2:
                    return sizeof(int);
            }
            return 0;
        }

        protected override void SerializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PosX1:
                    ByteBufferUtility.WriteInt(buffer, ref offset, posX1);
                    break;
                case ESerializeIndex.PosY1:
                    ByteBufferUtility.WriteInt(buffer, ref offset, posY1);
                    break;
                case ESerializeIndex.PosX2:
                    ByteBufferUtility.WriteInt(buffer, ref offset, posX2);
                    break;
                case ESerializeIndex.PosY2:
                    ByteBufferUtility.WriteInt(buffer, ref offset, posY2);
                    break;
            }
        }

        protected override void DeserializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PosX1:
                    posX1 = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
                case ESerializeIndex.PosY1:
                    posY1 = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
                case ESerializeIndex.PosX2:
                    posX2 = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
                case ESerializeIndex.PosY2:
                    posY2 = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
            }
        }

        private enum ESerializeIndex
        {
            PosX1,
            PosY1,
            PosX2,
            PosY2,

            Max
        }
    }
}
