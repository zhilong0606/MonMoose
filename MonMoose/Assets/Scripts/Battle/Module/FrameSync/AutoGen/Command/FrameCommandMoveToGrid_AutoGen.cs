namespace MonMoose.Battle
{
    public partial class FrameCommandMoveToGrid
    {
        public int entityUid;
        public int gridX;
        public int gridY;

        public override EFrameCommandType commandType
        {
            get { return EFrameCommandType.MoveToGrid; }
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
                case ESerializeIndex.GridX:
                    return gridX != default(int);
                case ESerializeIndex.GridY:
                    return gridY != default(int);
            }
            return false;
        }

        protected override int GetSizeOf(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.EntityUid:
                    return sizeof(int);
                case ESerializeIndex.GridX:
                    return sizeof(int);
                case ESerializeIndex.GridY:
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
                case ESerializeIndex.GridX:
                    ByteBufferUtility.WriteInt(buffer, ref offset, gridX);
                    break;
                case ESerializeIndex.GridY:
                    ByteBufferUtility.WriteInt(buffer, ref offset, gridY);
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
                case ESerializeIndex.GridX:
                    gridX = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
                case ESerializeIndex.GridY:
                    gridY = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
            }
        }

        private enum ESerializeIndex
        {
            EntityUid,
            GridX,
            GridY,

            Max
        }
    }
}
