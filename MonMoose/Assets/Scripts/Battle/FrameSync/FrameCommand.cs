using MonMoose.Core;

namespace MonMoose.Battle
{
    public abstract class FrameCommand : BattleObj
    {
        public abstract EFrameCommandType commandType { get; }

        public virtual int bufferLength
        {
            get { return 4; }
        }
        
        public virtual void Serialize(out byte[] buffer)
        {
            buffer = new byte[bufferLength];
            int offset = 0;
            OnSerialized(ref buffer, ref offset);
        }

        public void Deserialise(ref byte[] buffer, ref int offset)
        {
            OnDeserialised(ref buffer, ref offset);
        }

        protected virtual void OnSerialized(ref byte[] buffer, ref int offset)
        {
            //ByteBufferUtility.WriteInt(ref buffer, ref offset, playerId);
        }

        public virtual void OnDeserialised(ref byte[] buffer, ref int offset)
        {
            //playerId = ByteBufferUtility.ReadInt(ref buffer, ref offset);
        }

        public virtual void Excute(int playerId)
        {

        }
    }
}
