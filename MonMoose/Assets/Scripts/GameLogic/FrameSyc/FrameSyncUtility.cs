public static class FrameSyncUtility
{
    public static float MilliToFloat(int time)
    {
        return (float) time / FrameSyncDefine.Precision;
    }

    public static Fix32 MilliToFix(int milli)
    {
        return new Fix32(milli, 1000);
    }
}
