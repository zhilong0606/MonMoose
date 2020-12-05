using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public abstract class FrameMsgObj : BattleObj
    {
        public abstract bool isBitFlagConst { get;}

        public int GetByteBufferLength()
        {
            int size = 0;
            byte bitFlagCount = GetBitFlagCount();
            if (bitFlagCount > 0)
            {
                size += CalcByteCount(bitFlagCount);
                for (int i = 0; i < bitFlagCount; ++i)
                {
                    if (CheckValid(i))
                    {
                        size += GetSizeOf(i);
                    }
                }
            }
            return size;
        }

        public void Serialize(byte[] buffer, ref int offset)
        {
            byte bitFlagCount = GetBitFlagCount();
            if (isBitFlagConst)
            {
                ByteBufferUtility.WriteByte(buffer, ref offset, bitFlagCount);
            }
            ulong bitFlag = 0;
            for (int i = 0; i < bitFlagCount; ++i)
            {
                if (CheckValid(i))
                {
                    bitFlag |= 1UL << i;
                }
            }
            int flagByteCount = CalcByteCount(bitFlagCount);
            for (int i = 0; i < flagByteCount; ++i)
            {
                byte flagByte = (byte)(bitFlag >> (i * 8));
                ByteBufferUtility.WriteByte(buffer, ref offset, flagByte);
            }
            for (int i = 0; i < bitFlagCount; ++i)
            {
                if ((bitFlag & (1UL << i)) > 0)
                {
                    SerializeField(buffer, ref offset, i);
                }
            }
        }

        public void Deserialize(byte[] buffer, ref int offset)
        {
            byte bitFlagCount;
            if (isBitFlagConst)
            {
                bitFlagCount = GetBitFlagCount();
            }
            else
            {
                bitFlagCount = ByteBufferUtility.ReadByte(buffer, ref offset);
            }
            ulong bitFlag = 0;
            int flagByteCount = CalcByteCount(bitFlagCount);
            for (int i = 0; i < flagByteCount; ++i)
            {
                if (i != 0)
                {
                    bitFlag = bitFlag << 8;
                }
                byte flagByte = ByteBufferUtility.ReadByte(buffer, ref offset);
                bitFlag |= flagByte;
            }
            for (int i = 0; i < bitFlagCount; ++i)
            {
                if ((bitFlag & (1UL << i)) > 0)
                {
                    DeserializeField(buffer, ref offset, i);
                }
            }
        }

        private int CalcByteCount(int bitFlagCount)
        {
            if (bitFlagCount > 0)
            {
                return (bitFlagCount - 1) / 8 + 1;
            }
            return 0;
        }

        protected abstract byte GetBitFlagCount();
        protected abstract bool CheckValid(int index);
        protected abstract int GetSizeOf(int index);
        protected abstract void SerializeField(byte[] buffer, ref int offset, int index);
        protected abstract void DeserializeField(byte[] buffer, ref int offset, int index);
    }
}
