using System.Collections;
using System.Collections.Generic;

public struct Dcm32
{
    public static readonly Dcm32 MinValue = new Dcm32(Fix32.MinValue);
    public static readonly Dcm32 MaxValue = new Dcm32(Fix32.MaxValue);
    public static readonly Dcm32 zero = new Dcm32(0);
    public static readonly Dcm32 one = new Dcm32(1);
    public static readonly Dcm32 half = new Dcm32(1, 2);
    public static readonly Dcm32 Epsilon = new Dcm32(Fix32.Epsilon);

    internal Fix32 m_v;

    public Dcm32(int i)
    {
        m_v = new Fix32(i);
    }

    public Dcm32(int numerator, int denominator)
    {
        m_v = new Fix32(numerator, denominator);
    }

    public Dcm32(int integer, int numerator, int denominator)
    {
        m_v = new Fix32(integer, numerator, denominator);
    }

    internal Dcm32(Fix32 fix32)
    {
        m_v = fix32;
    }

    public static bool operator ==(Dcm32 lhs, Dcm32 rhs)
    {
        return lhs.m_v == rhs.m_v;
    }

    public static bool operator !=(Dcm32 lhs, Dcm32 rhs)
    {
        return lhs.m_v != rhs.m_v;
    }

    public static bool operator >(Dcm32 lhs, Dcm32 rhs)
    {
        return lhs.m_v > rhs.m_v;
    }

    public static bool operator >=(Dcm32 lhs, Dcm32 rhs)
    {
        return lhs.m_v >= rhs.m_v;
    }

    public static bool operator <(Dcm32 lhs, Dcm32 rhs)
    {
        return lhs.m_v < rhs.m_v;
    }

    public static bool operator <=(Dcm32 lhs, Dcm32 rhs)
    {
        return lhs.m_v <= rhs.m_v;
    }

    public static Dcm32 operator +(Dcm32 value)
    {
        return value;
    }

    public static Dcm32 operator -(Dcm32 value)
    {
        value.m_v = -value.m_v;
        return value;
    }

    public static Dcm32 operator +(Dcm32 lhs, Dcm32 rhs)
    {
        lhs.m_v = lhs.m_v + rhs.m_v;
        return lhs;
    }

    public static Dcm32 operator -(Dcm32 lhs, Dcm32 rhs)
    {
        lhs.m_v = lhs.m_v - rhs.m_v;
        return lhs;
    }

    public static Dcm32 operator *(Dcm32 lhs, Dcm32 rhs)
    {
        lhs.m_v = lhs.m_v * rhs.m_v;
        return lhs;
    }

    public static Dcm32 operator /(Dcm32 lhs, Dcm32 rhs)
    {
        lhs.m_v = lhs.m_v / rhs.m_v;
        return lhs;
    }

    public static Dcm32 operator <<(Dcm32 lhs, int rhs)
    {
        lhs.m_v = lhs.m_v << rhs;
        return lhs;
    }

    public static explicit operator float(Dcm32 value)
    {
        return (float)value.m_v;
    }

    public static explicit operator int(Dcm32 value)
    {
        return (int)value.m_v;
    }

    public static implicit operator Dcm32(int value)
    {
        return new Dcm32(value);
    }
}
