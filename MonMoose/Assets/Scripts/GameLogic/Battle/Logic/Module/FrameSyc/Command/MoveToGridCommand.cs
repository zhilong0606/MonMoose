namespace MonMoose.Logic.Battle
{
    public class MoveToGridCommand : FrameCommand
    {
        public int entityId;
        public GridPosition gridPos;

        public override EFrameCommandType commandType
        {
            get { return EFrameCommandType.MoveToGrid; }
        }

        public override int bufferLength
        {
            get { return base.bufferLength + 6; }
        }

        protected override void OnSerialized(ref byte[] buffer, ref int offset)
        {
            base.OnSerialized(ref buffer, ref offset);
            ByteBufferUtility.WriteInt(ref buffer, ref offset, entityId);
            byte x = (byte)gridPos.x;
            byte y = (byte)gridPos.y;
            ByteBufferUtility.WriteByte(ref buffer, ref offset, x);
            ByteBufferUtility.WriteByte(ref buffer, ref offset, y);
        }

        public override void OnDeserialised(ref byte[] buffer, ref int offset)
        {
            base.OnDeserialised(ref buffer, ref offset);
            entityId = ByteBufferUtility.ReadInt(ref buffer, ref offset);
            byte x = ByteBufferUtility.ReadByte(ref buffer, ref offset);
            byte y = ByteBufferUtility.ReadByte(ref buffer, ref offset);
            gridPos = new GridPosition(x, y);
        }

        public override void Excute(int playerId)
        {
            Entity entity = m_battleInstance.GetEntity(entityId);
            if (entity != null)
            {
                MoveComponent moveComponent = entity.GetComponent<MoveComponent>();
                if (moveComponent != null)
                {
                    moveComponent.MoveToGrid(gridPos);
                }
            }
        }
    }
}
