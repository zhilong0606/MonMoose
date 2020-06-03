using System;

namespace MonMoose.Logic.Battle
{
    public class FrameCommandGroup : BattleObj
    {
        private const int m_bitFlagSize = (FrameSyncDefine.CommandTypeCount - 1) / 8 + 1;
        private FrameCommand[] m_commands = new FrameCommand[FrameSyncDefine.CommandTypeCount];
        public int playerId;

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

        public void Serialize(out byte[] buffer)
        {
            int capacity = m_bitFlagSize;
            byte[] bitFlagBuffer = new byte[m_bitFlagSize];
            byte[][] serializeCache = new byte[FrameSyncDefine.CommandTypeCount][];
            for (int i = 0; i < m_commands.Length; ++i)
            {
                if (m_commands != null)
                {
                    m_commands[i].Serialize(out serializeCache[i]);
                    bitFlagBuffer[i / 8] |= (byte)(1 << (i % 8));
                    capacity += serializeCache[i].Length;
                }
            }
            buffer = new byte[capacity + 4];
            int offset = 0;
            Array.Copy(bitFlagBuffer, 0, buffer, 0, m_bitFlagSize);
            offset += m_bitFlagSize;
            for (int i = 0; i < serializeCache.Length; ++i)
            {
                if (serializeCache[i] != null || serializeCache[i].Length == 0)
                {
                    Array.Copy(serializeCache[i], 0, buffer, offset, serializeCache[i].Length);
                    offset += serializeCache[i].Length;
                }
            }
        }

        public void Deserialise(ref byte[] byteBuffer)
        {
            int offset = 0;
            long bitFlag = 0L;
            for (int i = 0; i < m_bitFlagSize; ++i)
            {
                bitFlag |= byteBuffer[offset] << (i << 8);
                offset++;
            }
            for (int i = 0; i < FrameSyncDefine.CommandTypeCount; i++)
            {
                if ((bitFlag & (1 << i)) > 0)
                {
                    FrameCommand command = FrameSyncFactory.CreateFrameCommand(m_battleInstance, (EFrameCommandType)i);
                    command.Deserialise(ref byteBuffer, ref offset);
                    AddCommand(command);
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
                    m_commands[i].Excute();
                    m_commands[i].Release();
                    m_commands[i] = null;
                }
            }
        }
    }
}
