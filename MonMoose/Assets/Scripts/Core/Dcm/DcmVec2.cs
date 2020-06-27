using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DcmVec2
{
    public static readonly DcmVec2 zero = new DcmVec2();
    public static readonly DcmVec2 one = new DcmVec2(1, 1);
    public static readonly DcmVec2 up = new DcmVec2(0, 1);
    public static readonly DcmVec2 down = new DcmVec2(0, -1);
    public static readonly DcmVec2 left = new DcmVec2(-1, 0);
    public static readonly DcmVec2 right = new DcmVec2(1, 0);

    public Dcm32 x;
    public Dcm32 y;

    public Dcm32 this[int index]
    {
        get
        {
            if (index == 0)
                return x;
            if (index == 1)
                return y;
            throw new IndexOutOfRangeException("Invalid Vector2 index!");
        }
        set
        {
            if (index == 0)
            {
                x = value;
            }
            else if (index == 1)
            {
                y = value;
            }
            else
            {
                throw new IndexOutOfRangeException("Invalid Vector2 index!");
            }
        }
    }

    public DcmVec2(Dcm32 x, Dcm32 y)
    {
        this.x = x;
        this.y = y;
    }

    public Dcm32 magnitude
    {
        get { return MathDcm.Sqrt(x * x + y * y); }
    }

    public Dcm32 sqrMagnitude
    {
        get { return x * x + y * y; }
    }

    public DcmVec2 normalized
    {
        get
        {
            if (x == 0 && y == 0)
                return zero;
            Dcm32 m = magnitude;
            if (m == 0)
                return zero;
            return new DcmVec2(x / m, y / m);
        }
    }

    public Dcm32 Dot(DcmVec2 rhs)
    {
        return x * rhs.x + y * rhs.y;
    }

    public Dcm32 Cross(DcmVec2 rhs)
    {
        return x * rhs.y - y * rhs.x;
    }

    public static DcmVec2 operator +(DcmVec2 rhs)
    {
        return rhs;
    }

    public static DcmVec2 operator -(DcmVec2 rhs)
    {
        rhs.x = -rhs.x;
        rhs.y = -rhs.y;
        return rhs;
    }

    public static DcmVec2 operator +(DcmVec2 lhs, DcmVec2 rhs)
    {
        lhs.x += rhs.x;
        lhs.y += rhs.y;
        return lhs;
    }

    public static DcmVec2 operator -(DcmVec2 lhs, DcmVec2 rhs)
    {
        lhs.x -= rhs.x;
        lhs.y -= rhs.y;
        return lhs;
    }

    public static DcmVec2 operator +(DcmVec2 lhs, Dcm32 rhs)
    {
        lhs.x += rhs;
        lhs.y += rhs;
        return lhs;
    }

    public static DcmVec2 operator +(Dcm32 lhs, DcmVec2 rhs)
    {
        rhs.x += lhs;
        rhs.y += lhs;
        return rhs;
    }

    public static DcmVec2 operator -(DcmVec2 lhs, Dcm32 rhs)
    {
        lhs.x -= rhs;
        lhs.y -= rhs;
        return lhs;
    }

    public static DcmVec2 operator *(DcmVec2 lhs, DcmVec2 rhs)
    {
        lhs.x *= rhs.x;
        lhs.y *= rhs.y;
        return lhs;
    }

    public static DcmVec2 operator *(DcmVec2 lhs, Dcm32 rhs)
    {
        lhs.x *= rhs;
        lhs.y *= rhs;
        return lhs;
    }

    public static DcmVec2 operator *(Dcm32 lhs, DcmVec2 rhs)
    {
        rhs.x *= lhs;
        rhs.y *= lhs;
        return rhs;
    }

    public static DcmVec2 operator /(DcmVec2 lhs, DcmVec2 rhs)
    {
        lhs.x /= rhs.x;
        lhs.y /= rhs.y;
        return lhs;
    }

    public static DcmVec2 operator /(DcmVec2 lhs, Dcm32 rhs)
    {
        lhs.x /= rhs;
        lhs.y /= rhs;
        return lhs;
    }

    public static bool operator ==(DcmVec2 lhs, DcmVec2 rhs)
    {
        return (lhs - rhs).sqrMagnitude < Dcm32.Epsilon;
    }

    public static bool operator !=(DcmVec2 lhs, DcmVec2 rhs)
    {
        return !(lhs == rhs);
    }

    public static Dcm32 Dot(DcmVec2 lhs, DcmVec2 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y;
    }
}
