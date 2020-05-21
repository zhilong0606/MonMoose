/* FixedPointy - A simple fixed-point math library for C#.
 * 
 * Copyright (c) 2013 Jameson Ernst
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;

public static partial class MathFix
{
    public static readonly Fix32 PI = new Fix32(3141593, 1000000);
    public static readonly Fix32 Deg2Rad = new Fix32(1745329, 100000000);
    public static readonly Fix32 Rad2Deg = new Fix32(5729578, 100000);
    public static readonly Fix32 E;
    static Fix32 _log2_E;
    static Fix32 _log2_10;
    static Fix32 _ln2;
    static Fix32 _log10_2;
    static Fix32[] _quarterSine;
    static Fix32[] _cordicAngles;
    static Fix32[] _cordicGains;

    static MathFix()
    {
        if (_quarterSineResPower >= Fix32.FractionalBits)
            throw new Exception("_quarterSineResPower must be less than Fix.FractionalBits.");
        if (_quarterSineConsts.Length != 90 * (1 << _quarterSineResPower) + 1)
            throw new Exception("_quarterSineConst.Length must be 90 * 2^(_quarterSineResPower) + 1.");
        
        E = _eConst;
        _log2_E = _log2_EConst;
        _log2_10 = _log2_10Const;
        _ln2 = _ln2Const;
        _log10_2 = _log10_2Const;

        _quarterSine = Array.ConvertAll(_quarterSineConsts, c => (Fix32)c);
        _cordicAngles = Array.ConvertAll(_cordicAngleConsts, c => (Fix32)c);
        _cordicGains = Array.ConvertAll(_cordicGainConsts, c => (Fix32)c);
    }

    public static Fix32 Abs(Fix32 value)
    {
        return value.raw < 0 ? Fix32.Raw(-value.raw) : value;
    }

    public static Fix32 Clamp(Fix32 value, Fix32 min, Fix32 max)
    {
        if (value < min)
            value = min;
        else if (value > max)
            value = max;
        return value;
    }

    public static Fix32 Clamp01(Fix32 value)
    {
        if (value < 0)
            return Fix32.zero;
        if (value > 1)
            return Fix32.one;
        return value;
    }

    public static Fix32 Sign(Fix32 value)
    {
        if (value < 0)
            return -1;
        else if (value > 0)
            return 1;
        else
            return 0;
    }

    public static Fix32 Ceiling(Fix32 value)
    {
        return Fix32.Raw((value.raw + Fix32.FractionMask) & Fix32.IntegerMask);
    }

    public static Fix32 Floor(Fix32 value)
    {
        return Fix32.Raw(value.raw & Fix32.IntegerMask);
    }

    public static Fix32 Truncate(Fix32 value)
    {
        if (value < 0)
            return Fix32.Raw((value.raw + Fix32.FractionRange) & Fix32.IntegerMask);
        else
            return Fix32.Raw(value.raw & Fix32.IntegerMask);
    }

    public static Fix32 Round(Fix32 value)
    {
        return Fix32.Raw((value.raw + (Fix32.FractionRange >> 1)) & ~Fix32.FractionMask);
    }

    public static Fix32 Min(Fix32 v1, Fix32 v2)
    {
        return v1 < v2 ? v1 : v2;
    }

    public static Fix32 Max(Fix32 v1, Fix32 v2)
    {
        return v1 > v2 ? v1 : v2;
    }

    public static Fix32 Sqrt(Fix32 value)
    {
        if (value.raw < 0)
            throw new ArgumentOutOfRangeException("value", "Value must be non-negative.");
        if (value.raw == 0)
            return 0;

        return Fix32.Raw((int)(SqrtULong((ulong)value.raw << (Fix32.FractionalBits + 2)) + 1) >> 1);
    }

    internal static uint SqrtULong(ulong N)
    {
        ulong x = 1L << ((31 + (Fix32.FractionalBits + 2) + 1) / 2);
        while (true)
        {
            ulong y = (x + N / x) >> 1;
            if (y >= x)
                return (uint)x;
            x = y;
        }
    }

    public static Fix32 Sin(Fix32 degrees)
    {
        return CosRaw(degrees.raw - (90 << Fix32.FractionalBits));
    }

    public static Fix32 Cos(Fix32 degrees)
    {
        return CosRaw(degrees.raw);
    }

    static Fix32 CosRaw(int raw)
    {
        raw = raw < 0 ? -raw : raw;
        int t = raw & ((1 << (Fix32.FractionalBits - _quarterSineResPower)) - 1);
        raw = (raw >> (Fix32.FractionalBits - _quarterSineResPower));

        if (t == 0)
            return CosRawLookup(raw);

        Fix32 v1 = CosRawLookup(raw);
        Fix32 v2 = CosRawLookup(raw + 1);

        return Fix32.Raw(
            (int)(
                (
                    (long)v1.raw * ((1 << (Fix32.FractionalBits - _quarterSineResPower)) - t)
                    + (long)v2.raw * t
                    + (1 << (Fix32.FractionalBits - _quarterSineResPower - 1))
                    )
                >> (Fix32.FractionalBits - _quarterSineResPower)
                )
            );
    }

    static Fix32 CosRawLookup(int raw)
    {
        raw %= 360 * (1 << _quarterSineResPower);

        if (raw < 90 * (1 << _quarterSineResPower))
        {
            return _quarterSine[90 * (1 << _quarterSineResPower) - raw];
        }
        else if (raw < 180 * (1 << _quarterSineResPower))
        {
            raw -= 90 * (1 << _quarterSineResPower);
            return -_quarterSine[raw];
        }
        else if (raw < 270 * (1 << _quarterSineResPower))
        {
            raw -= 180 * (1 << _quarterSineResPower);
            return -_quarterSine[90 * (1 << _quarterSineResPower) - raw];
        }
        else
        {
            raw -= 270 * (1 << _quarterSineResPower);
            return _quarterSine[raw];
        }
    }

    public static Fix32 Tan(Fix32 degrees)
    {
        return Sin(degrees) / Cos(degrees);
    }

    public static Fix32 Asin(Fix32 value)
    {
        return Atan2(value, Sqrt((1 + value) * (1 - value)));
    }

    public static Fix32 Acos(Fix32 value)
    {
        return Atan2(Sqrt((1 + value) * (1 - value)), value);
    }

    public static Fix32 Atan(Fix32 value)
    {
        return Atan2(value, 1);
    }

    public static Fix32 Atan2(Fix32 y, Fix32 x)
    {
        if (x == 0 && y == 0)
            throw new ArgumentOutOfRangeException("y and x cannot both be 0.");

        Fix32 angle = 0;
        Fix32 xNew, yNew;

        if (x < 0)
        {
            if (y < 0)
            {
                xNew = -y;
                yNew = x;
                angle = -90;
            }
            else if (y > 0)
            {
                xNew = y;
                yNew = -x;
                angle = 90;
            }
            else
            {
                xNew = x;
                yNew = y;
                angle = 180;
            }
            x = xNew;
            y = yNew;
        }

        for (int i = 0; i < Fix32.FractionalBits + 2; i++)
        {
            if (y > 0)
            {
                xNew = x + (y >> i);
                yNew = y - (x >> i);
                angle += _cordicAngles[i];
            }
            else if (y < 0)
            {
                xNew = x - (y >> i);
                yNew = y + (x >> i);
                angle -= _cordicAngles[i];
            }
            else
                break;

            x = xNew;
            y = yNew;
        }

        return angle;
    }

    public static Fix32 Exp(Fix32 value)
    {
        return Pow(E, value);
    }

    public static Fix32 Pow(Fix32 b, Fix32 exp)
    {
        if (b == 1 || exp == 0)
            return 1;

        int intPow;
        Fix32 intFactor;
        if ((exp.raw & Fix32.FractionMask) == 0)
        {
            intPow = (int)((exp.raw + (Fix32.FractionRange >> 1)) >> Fix32.FractionalBits);
            Fix32 t;
            int p;
            if (intPow < 0)
            {
                t = 1 / b;
                p = -intPow;
            }
            else
            {
                t = b;
                p = intPow;
            }

            intFactor = 1;
            while (p > 0)
            {
                if ((p & 1) != 0)
                    intFactor *= t;
                t *= t;
                p >>= 1;
            }

            return intFactor;
        }

        exp *= Log(b, 2);
        b = 2;
        intPow = (int)((exp.raw + (Fix32.FractionRange >> 1)) >> Fix32.FractionalBits);
        intFactor = intPow < 0 ? Fix32.one >> -intPow : Fix32.one << intPow;

        long x = (
            ((exp.raw - (intPow << Fix32.FractionalBits)) * _ln2Const.Raw)
            + (Fix32.FractionRange >> 1)
            ) >> Fix32.FractionalBits;
        if (x == 0)
            return intFactor;

        long fracFactor = x;
        long xa = x;
        for (int i = 2; i < _invFactConsts.Length; i++)
        {
            if (xa == 0)
                break;
            xa *= x;
            xa += (1L << (32 - 1));
            xa >>= 32;
            long p = xa * _invFactConsts[i].Raw;
            p += (1L << (32 - 1));
            p >>= 32;
            fracFactor += p;
        }

        return Fix32.Raw((int)((((long)intFactor.raw * fracFactor + (1L << (32 - 1))) >> 32) + intFactor.raw));
    }

    public static Fix32 Log(Fix32 value)
    {
        return Log2(value) * _ln2;
    }

    public static Fix32 Log(Fix32 value, Fix32 b)
    {
        if (b == 2)
            return Log2(value);
        else if (b == E)
            return Log(value);
        else if (b == 10)
            return Log10(value);
        else
            return Log2(value) / Log2(b);
    }

    public static Fix32 Log10(Fix32 value)
    {
        return Log2(value) * _log10_2;
    }

    static Fix32 Log2(Fix32 value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException("value", "Value must be positive.");

        uint x = (uint)value.raw;
        uint b = 1U << (Fix32.FractionalBits - 1);
        uint y = 0;

        while (x < 1U << Fix32.FractionalBits)
        {
            x <<= 1;
            y -= 1U << Fix32.FractionalBits;
        }

        while (x >= 2U << Fix32.FractionalBits)
        {
            x >>= 1;
            y += 1U << Fix32.FractionalBits;
        }

        ulong z = x;

        for (int i = 0; i < Fix32.FractionalBits; i++)
        {
            z = z * z >> Fix32.FractionalBits;
            if (z >= 2U << Fix32.FractionalBits)
            {
                z >>= 1;
                y += b;
            }
            b >>= 1;
        }

        return Fix32.Raw((int)y);
    }
}

