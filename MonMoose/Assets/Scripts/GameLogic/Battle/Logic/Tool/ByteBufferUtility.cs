namespace MonMoose.Logic.Battle
{
    public static class ByteBufferUtility
    {
        public static void WriteShort(ref byte[] buffer, ref int offset, short value)
        {
            for (int i = 0; i < 2; ++i)
            {
                buffer[offset] = (byte)(value >> (i * 8));
                offset++;
            }
        }

        public static short ReadShort(ref byte[] buffer, ref int offset)
        {
            short value = 0;
            for (int i = 0; i < 2; ++i)
            {
                value |= (short)(buffer[offset] << (i * 8));
                offset++;
            }

            return value;
        }

        public static void WriteInt(ref byte[] buffer, ref int offset, int value)
        {
            for (int i = 0; i < 4; ++i)
            {
                buffer[offset] = (byte)(value >> (i * 8));
                offset++;
            }
        }

        public static int ReadInt(ref byte[] buffer, ref int offset)
        {
            int value = 0;
            for (int i = 0; i < 4; ++i)
            {
                value |= buffer[offset] << (i * 8);
                offset++;
            }

            return value;
        }
    }
}
