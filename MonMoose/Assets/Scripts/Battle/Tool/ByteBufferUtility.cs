namespace MonMoose.Battle
{
    public static class ByteBufferUtility
    {
        public static void WriteShort(byte[] buffer, ref int offset, short value)
        {
            for (int i = 0; i < 2; ++i)
            {
                buffer[offset] = (byte)(value >> (i * 8));
                offset++;
            }
        }

        public static short ReadShort(byte[] buffer, ref int offset)
        {
            short value = 0;
            for (int i = 0; i < 2; ++i)
            {
                value |= (short)(buffer[offset] << (i * 8));
                offset++;
            }

            return value;
        }

        public static void WriteInt(byte[] buffer, ref int offset, int value)
        {
            for (int i = 0; i < 4; ++i)
            {
                buffer[offset] = (byte)(value >> (i * 8));
                offset++;
            }
        }

        public static int ReadInt(byte[] buffer, ref int offset)
        {
            int value = 0;
            for (int i = 0; i < 4; ++i)
            {
                value |= buffer[offset] << (i * 8);
                offset++;
            }

            return value;
        }

        public static void WriteByte(byte[] buffer, ref int offset, byte value)
        {
            buffer[offset] = value;
            offset++;
        }

        public static byte ReadByte(byte[] buffer, ref int offset)
        {
            byte value = buffer[offset];
            offset++;
            return value;
        }
    }
}
