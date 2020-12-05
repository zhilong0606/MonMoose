﻿using System;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public class FrameCommandUnion : FrameMsgObj
    {
        private int playerId;
        private FrameCommand[] m_commands = new FrameCommand[(int)EFrameCommandType.Max];

        public override bool isBitFlagConst
        {
            get { return true; }
        }

        public override void OnRelease()
        {
            for (int i = 0; i < m_commands.Length; ++i)
            {
                if (m_commands != null)
                {
                    m_commands[i].Release();
                    m_commands[i] = null;
                }
            }
        }

        public void AddCommand(FrameCommand command)
        {
            int index = (int)command.commandType;
            if (m_commands[index] != null)
            {
                m_commands[index].Release();
            }
            m_commands[index] = command;
        }

        public void Excute()
        {
            for (int i = 0; i < m_commands.Length; ++i)
            {
                if (m_commands[i] != null)
                {
                    m_commands[i].Excute(playerId);
                }
            }
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
                    return playerId == default(int);
                default:
                    return m_commands[index - (int)ESerializeIndex.CmdStart] != null;
            }
        }

        protected override int GetSizeOf(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PlayerId:
                    return sizeof(int);
                default:
                    return m_commands[index - (int)ESerializeIndex.CmdStart].GetByteBufferLength();
            }
        }

        protected override void SerializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.PlayerId:
                    ByteBufferUtility.WriteInt(buffer, ref offset, playerId);
                    break;
                default:
                    m_commands[index - (int)ESerializeIndex.CmdStart].Serialize(buffer, ref offset);
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
                default:
                    m_commands[index - (int)ESerializeIndex.CmdStart].Deserialize(buffer, ref offset);
                    break;
            }
        }

        private enum ESerializeIndex
        {
            PlayerId,
            CmdStart,

            Max = CmdStart + (int)EFrameCommandType.Max,
        }
    }
}
