using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public class FrameCommandWrap : FrameMsgObj
    {
        public int playerId;
        public byte cmdType;
        public FrameCommand cmd;

        public override bool isBitFlagConst
        {
            get { return true; }
        }

        public override void OnRelease()
        {
            base.OnRelease();
            playerId = default(int);
        }

        protected override byte GetBitFlagCount()
        {
            return (int)ESerializeIndex.Max;
        }

        protected override bool CheckValid(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PlayerId:
                    return playerId != default(int);
                case ESerializeIndex.CmdType:
                    return cmdType != default(byte);
                case ESerializeIndex.Cmd:
                    return cmd != null;
            }
            return false;
        }

        protected override int GetSizeOf(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PlayerId:
                    return sizeof(int);
                case ESerializeIndex.CmdType:
                    return sizeof(byte);
                case ESerializeIndex.Cmd:
                    return cmd.GetByteBufferLength();
            }
            return 0;
        }

        protected override void SerializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PlayerId:
                    ByteBufferUtility.WriteInt(buffer, ref offset, playerId);
                    break;
                case ESerializeIndex.CmdType:
                    ByteBufferUtility.WriteByte(buffer, ref offset, cmdType);
                    break;
                case ESerializeIndex.Cmd:
                    cmd.Serialize(buffer, ref offset);
                    break;
            }
        }

        protected override void DeserializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PlayerId:
                    playerId = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
                case ESerializeIndex.CmdType:
                    cmdType = ByteBufferUtility.ReadByte(buffer, ref offset);
                    break;
                case ESerializeIndex.Cmd:
                    cmd = FrameCommandFactory.CreateCommand(m_battleInstance, (EFrameCommandType)cmdType);
                    cmd.Deserialize(buffer, ref offset);
                    break;
            }
        }
        
        private enum ESerializeIndex
        {
            PlayerId,
            CmdType,
            Cmd,
            Max
        }
    }
}
