using System.Collections;
using System.Collections.Generic;

public static class MathDcm
{

    public static Dcm32 Abs(Dcm32 value)
    {
        if (value < 0)
        {
            return -value;
        }
        return value;
    }

    public static Dcm32 Clamp(Dcm32 value, Dcm32 min, Dcm32 max)
    {
        if (value < min)
            value = min;
        else if (value > max)
            value = max;
        return value;
    }

    public static Dcm32 Clamp01(Dcm32 value)
    {
        if (value < 0)
            return Dcm32.zero;
        if (value > 1)
            return Dcm32.one;
        return value;
    }

    public static int Sign(Dcm32 value)
    {
        if (value < 0)
            return -1;
        else if (value > 0)
            return 1;
        else
            return 0;
    }

    public static Dcm32 Sqrt(Dcm32 value)
    {
        return new Dcm32(MathFix.Sqrt(value.m_v));
    }
}
