using System;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public class FrameCommandGroup : ClassPoolObj
    {
        public const int bitFlagSize = (FrameSyncDefine.CommandTypeCount - 1) / 8 + 1;
        public FrameCommand[] commands = new FrameCommand[FrameSyncDefine.CommandTypeCount];
        public int playerID;

        public override void OnRelease()
        {
            for (int i = 0; i < commands.Length; ++i)
            {
                if (commands != null)
                {
                    commands[i].Release();
                    commands[i] = null;
                }
            }
        }

        public void Serialize(out byte[] buffer)
        {
            int capacity = bitFlagSize;
            byte[] bitFlagBuffer = new byte[bitFlagSize];
            byte[][] serializeCache = new byte[FrameSyncDefine.CommandTypeCount][];
            for (int i = 0; i < commands.Length; ++i)
            {
                if (commands != null)
                {
                    commands[i].Serialize(out serializeCache[i]);
                    bitFlagBuffer[i / 8] |= (byte)(1 << (i % 8));
                    capacity += serializeCache[i].Length;
                }
            }
            buffer = new byte[capacity + 4];
            int offset = 0;
            ByteBufferUtility.WriteInt(ref buffer, ref offset, playerID);
            Array.Copy(bitFlagBuffer, 0, buffer, 0, bitFlagSize);
            offset += bitFlagSize;
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
            playerID = ByteBufferUtility.ReadInt(ref byteBuffer, ref offset);
            long bitFlag = 0L;
            for (int i = 0; i < bitFlagSize; ++i)
            {
                bitFlag |= byteBuffer[offset] << (i << 8);
                offset++;
            }
            for (int i = 0; i < FrameSyncDefine.CommandTypeCount; i++)
            {
                if ((bitFlag & (1 << i)) > 0)
                {
                    FrameCommand command = FrameCommand.GetCommand((EFrameCommandType)i);
                    command.Deserialise(ref byteBuffer, ref offset);
                    AddCommand(command);
                }
            }
        }

        public void AddCommand(FrameCommand command)
        {
            int index = (int)command.commandType;
            if (commands[index] != null)
            {
                commands[index].Release();
            }
            commands[index] = command;
        }

        public void Excute()
        {
            for (int i = 0; i < commands.Length; ++i)
            {
                if (commands[i] != null)
                {
                    commands[i].Excute();
                    commands[i].Release();
                    commands[i] = null;
                }
            }
        }
    }
}
