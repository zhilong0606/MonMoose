namespace MonMoose.Battle
{
    public partial class FrameCommandFormationRetreat
    {
        public int entityUid;

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
                case ESerializeIndex.EntityUid:
                    return entityUid != default(int);
            }
            return false;
        }

        protected override int GetSizeOf(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.EntityUid:
                    return sizeof(int);
            }
            return 0;
        }

        protected override void SerializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.EntityUid:
                    ByteBufferUtility.WriteInt(buffer, ref offset, entityUid);
                    break;
            }
        }

        protected override void DeserializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.EntityUid:
                    entityUid = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
            }
        }

        private enum ESerializeIndex
        {
            EntityUid,

            Max
        }
    }
}
