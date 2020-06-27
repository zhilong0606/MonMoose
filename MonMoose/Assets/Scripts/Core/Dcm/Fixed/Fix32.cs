using System;
using System.Text;

[Serializable]
internal struct Fix32
{
    public const int FractionalBits = 10;
    public const int IntegerBits = sizeof (int) * 8 - FractionalBits;
    public const int FractionMask = (int)(uint.MaxValue >> IntegerBits);
    public const int IntegerMask = -1 & ~FractionMask;
    public const int FractionRange = FractionMask + 1;
    public const int MinInt32 = int.MinValue >> FractionalBits;
    public const int MaxInt32 = int.MaxValue >> FractionalBits;

    public static readonly Fix32 MinValue = Raw(int.MinValue);
    public static readonly Fix32 MaxValue = Raw(int.MaxValue);
    public static readonly Fix32 zero = new Fix32(0);
    public static readonly Fix32 one = new Fix32(1);
    public static readonly Fix32 half = new Fix32(1, 2);
    public static readonly Fix32 Epsilon = Raw(1);
    
    public int raw;

    public Fix32(int i)
    {
        raw = i * FractionRange;
    }

    public Fix32(float f)
    {
        int d = 1 << FractionalBits;
        int n = (int)(f * d);
        raw = CalcRatioRaw(n, d);
    }

    public Fix32(int numerator, int denominator)
    {
        checked
        {
            raw = CalcRatioRaw(numerator, denominator);
        }
    }

    public Fix32(int integer, int numerator, int denominator)
    {
        raw = CalcMixRaw(integer, numerator, denominator);
    }

    public static int CalcRatioRaw(int numerator, int denominator)
    {
        checked
        {
            return (int)((((long)numerator << (FractionalBits + 1)) / denominator + 1) >> 1);
        }
    }

    public static int CalcMixRaw(int integer, int numerator, int denominator)
    {
        if (numerator < 0 || denominator < 0)
            throw new ArgumentException("Ratio must be positive.");
        checked
        {
            int fraction = (int)((long)FractionRange * numerator / denominator) & FractionMask;
            fraction = integer < 0 ? -fraction : fraction;
            return (integer << FractionalBits) + fraction;
        }
    }

    public static Fix32 Raw(int raw)
    {
        return new Fix32
        {
            raw = raw
        };
    }

    public static explicit operator double(Fix32 value)
    {
        return (value.raw >> FractionalBits) + (value.raw & FractionMask) / (double)FractionRange;
    }

    public static explicit operator float(Fix32 value)
    {
        return (float)(double)value;
    }

    public static explicit operator int(Fix32 value)
    {
        if (value.raw > 0)
        {
            return value.raw >> FractionalBits;
        }
        return (value.raw + FractionMask) >> FractionalBits;
    }

    public static implicit operator Fix32(int value)
    {
        return new Fix32(value);
    }

    public static implicit operator Fix32(float value)
    {
        return new Fix32(value);
    }

    public static bool operator ==(Fix32 lhs, Fix32 rhs)
    {
        return lhs.raw == rhs.raw;
    }

    public static bool operator !=(Fix32 lhs, Fix32 rhs)
    {
        return lhs.raw != rhs.raw;
    }

    public static bool operator >(Fix32 lhs, Fix32 rhs)
    {
        return lhs.raw > rhs.raw;
    }

    public static bool operator >=(Fix32 lhs, Fix32 rhs)
    {
        return lhs.raw >= rhs.raw;
    }

    public static bool operator <(Fix32 lhs, Fix32 rhs)
    {
        return lhs.raw < rhs.raw;
    }

    public static bool operator <=(Fix32 lhs, Fix32 rhs)
    {
        return lhs.raw <= rhs.raw;
    }

    public static Fix32 operator +(Fix32 value)
    {
        return value;
    }

    public static Fix32 operator -(Fix32 value)
    {
        value.raw = -value.raw;
        return value;
    }

    public static Fix32 operator +(Fix32 lhs, Fix32 rhs)
    {
        checked
        {
            lhs.raw += rhs.raw;
        }
        return lhs;
    }

    public static Fix32 operator -(Fix32 lhs, Fix32 rhs)
    {
        checked
        {
            lhs.raw -= rhs.raw;
        }
        return lhs;
    }

    public static Fix32 operator *(Fix32 lhs, Fix32 rhs)
    {
        checked
        {
            lhs.raw = (int)(((long)lhs.raw * rhs.raw + (FractionRange >> 1)) >> FractionalBits);
        }
        return lhs;
    }

    public static Fix32 operator /(Fix32 lhs, Fix32 rhs)
    {
        checked
        {
            lhs.raw = (int)((((long)lhs.raw << (FractionalBits + 1)) / rhs.raw + 1) >> 1);
        }
        return lhs;
    }

    public static Fix32 operator <<(Fix32 lhs, int rhs)
    {
        checked
        {
            lhs.raw = lhs.raw << rhs;
        }
        return lhs;
    }

    public static Fix32 operator >>(Fix32 lhs, int rhs)
    {
        lhs.raw = lhs.raw >> rhs;
        return lhs;
    }

    public override bool Equals(object obj)
    {
        return obj is Fix32 && (Fix32)obj == this;
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