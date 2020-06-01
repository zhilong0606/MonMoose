using System;
using System.Text;

public struct Fix64
{
    public const int FractionalBits = Fix32.FractionalBits * 2;
    public const int IntegerBits = sizeof(long) * 8 - FractionalBits;
    public const long FractionMask = (long)(ulong.MaxValue >> IntegerBits);
    public const long IntegerMask = -1 & ~FractionMask;
    public const long FractionRange = FractionMask + 1;
    public const long MinInt64 = long.MinValue >> FractionalBits;
    public const long MaxInt64 = long.MaxValue >> FractionalBits;

    public static readonly Fix64 MinValue = Raw(long.MinValue);
    public static readonly Fix64 MaxValue = Raw(long.MaxValue);
    public static readonly Fix64 Zero = new Fix64(0);
    public static readonly Fix64 One = new Fix64(1);
    public static readonly Fix64 Epsilon = Raw(1);

    public long raw;

    public Fix64(int i)
    {
        raw = i * FractionRange;
    }

    public Fix64(float f)
    {
        int i = (int)f;
        int d = 1 << FractionalBits;
        int n = (int)((f - i) * d);
        raw = CalcMixRaw(i, n, d);
    }

    public Fix64(Fix32 f)
    {
        this = Raw((long)f.raw * Fix32.one.raw);
    }

    public static long CalcMixRaw(int integer, int numerator, int denominator)
    {
        if (numerator < 0 || denominator < 0)
            throw new ArgumentException("Ratio must be positive.");
        checked
        {
            long fraction = (FractionRange * numerator / denominator) & FractionMask;
            fraction = integer < 0 ? -fraction : fraction;
            return (integer << FractionalBits) + fraction;
        }
    }

    public static Fix64 Raw(long raw)
    {
        return new Fix64
        {
            raw = raw
        };
    }

    public static explicit operator double(Fix64 value)
    {
        return (value.raw >> FractionalBits) + (value.raw & FractionMask) / (double)FractionRange;
    }

    public static explicit operator float(Fix64 value)
    {
        return (float)(double)value;
    }

    public static explicit operator long(Fix64 value)
    {
        if (value.raw > 0)
        {
            return value.raw >> FractionalBits;
        }
        return (value.raw + FractionMask) >> FractionalBits;
    }

    public static implicit operator Fix64(long value)
    {
        return new Fix64(value);
    }

    public static implicit operator Fix64(Fix32 value)
    {
        return Raw((long)value.raw * Fix32.one.raw);
    }

    public static implicit operator Fix32(Fix64 value)
    {
        return Fix32.Raw((int)value.raw / Fix32.one.raw);
    }

    public static bool operator ==(Fix64 lhs, Fix64 rhs)
    {
        return lhs.raw == rhs.raw;
    }

    public static bool operator !=(Fix64 lhs, Fix64 rhs)
    {
        return lhs.raw != rhs.raw;
    }

    public static bool operator >(Fix64 lhs, Fix64 rhs)
    {
        return lhs.raw > rhs.raw;
    }

    public static bool operator >=(Fix64 lhs, Fix64 rhs)
    {
        return lhs.raw >= rhs.raw;
    }

    public static bool operator <(Fix64 lhs, Fix64 rhs)
    {
        return lhs.raw < rhs.raw;
    }

    public static bool operator <=(Fix64 lhs, Fix64 rhs)
    {
        return lhs.raw <= rhs.raw;
    }

    public static Fix64 operator +(Fix64 value)
    {
        return value;
    }

    public static Fix64 operator -(Fix64 value)
    {
        value.raw = -value.raw;
        return value;
    }

    public static Fix64 operator +(Fix64 lhs, Fix64 rhs)
    {
        checked
        {
            lhs.raw += rhs.raw;
        }
        return lhs;
    }

    public static Fix64 operator -(Fix64 lhs, Fix64 rhs)
    {
        lhs.raw -= rhs.raw;
        return lhs;
    }

    public static Fix64 operator *(Fix64 lhs, Fix64 rhs)
    {
        checked
        {
            lhs.raw = (lhs.raw * rhs.raw + (FractionRange >> 1)) >> FractionalBits;
        }
        return lhs;
    }

    public static Fix64 operator *(Fix64 lhs, Fix32 rhs)
    {
        checked
        {
            lhs.raw = (lhs.raw * rhs.raw + (FractionRange >> 1)) >> Fix32.FractionalBits;
        }
        return lhs;
    }

    public static Fix64 operator *(Fix32 lhs, Fix64 rhs)
    {
        checked
        {
            rhs.raw = (rhs.raw * lhs.raw + (FractionRange >> 1)) >> Fix32.FractionalBits;
        }
        return lhs;
    }

    public static Fix64 operator /(Fix64 lhs, Fix64 rhs)
    {
        checked
        {
            lhs.raw = ((lhs.raw << (FractionalBits + 1)) / rhs.raw + 1) >> 1;
        }
        return lhs;
    }

    public static Fix64 operator <<(Fix64 lhs, int rhs)
    {
        checked
        {
            lhs.raw = lhs.raw << rhs;
        }
        return lhs;
    }

    public static Fix64 operator >>(Fix64 lhs, int rhs)
    {
        lhs.raw = lhs.raw >> rhs;
        return lhs;
    }

    public override bool Equals(object obj)
    {
        return obj is Fix64 && (Fix64)obj == this;
    }

    public override int GetHashCode()
    {
        return raw.GetHashCode();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        if (raw < 0)
            sb.Append(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NegativeSign);
        int abs = (int)this;
        abs = abs < 0 ? -abs : abs;
        sb.Append(abs.ToString());
        ulong fraction = (ulong)(raw & FractionMask);
        if (fraction == 0)
            return sb.ToString();

        fraction = raw < 0 ? FractionRange - fraction : fraction;
        fraction *= 1000000L;
        fraction += FractionRange >> 1;
        fraction >>= FractionalBits;

        sb.Append(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        sb.Append(fraction.ToString("D6").TrimEnd('0'));
        return sb.ToString();
    }
}