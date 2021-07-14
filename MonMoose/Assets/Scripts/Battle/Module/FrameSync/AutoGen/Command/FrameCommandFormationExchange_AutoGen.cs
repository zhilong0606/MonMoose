namespace MonMoose.Battle
{
    public partial class FrameCommandFormationExchange
    {
        public int entityUid1;
        public int entityUid2;

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
                case ESerializeIndex.EntityUid1:
                    return entityUid1 != default(int);
                case ESerializeIndex.EntityUid2:
                    return entityUid2 != default(int);
            }
            return false;
        }

        protected override int GetSizeOf(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.EntityUid1:
                    return sizeof(int);
                case ESerializeIndex.EntityUid2:
                    return sizeof(int);
            }
            return 0;
        }

        protected override void SerializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.EntityUid1:
                    ByteBufferUtility.WriteInt(buffer, ref offset, entityUid1);
                    break;
                case ESerializeIndex.EntityUid2:
                    ByteBufferUtility.WriteInt(buffer, ref offset, entityUid2);
                    break;
            }
        }

        protected override void DeserializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.EntityUid1:
                    entityUid1 = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
                case ESerializeIndex.EntityUid2:
                    entityUid2 = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
            }
        }

        private enum ESerializeIndex
        {
            EntityUid1,
            EntityUid2,

            Max
        }
    }
}
