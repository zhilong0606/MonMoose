using System;

public struct FixVec2 : IEquatable<FixVec2>
{
    public static readonly FixVec2 zeroVector = new FixVec2();
    public static readonly FixVec2 oneVector = new FixVec2(1, 1);
    public static readonly FixVec2 upVector = new FixVec2(0, 1);
    public static readonly FixVec2 downVector = new FixVec2(0, -1);
    public static readonly FixVec2 leftVector = new FixVec2(-1, 0);
    public static readonly FixVec2 rightVector = new FixVec2(1, 0);

    public static FixVec2 zero { get { return zeroVector; } }
    public static FixVec2 one { get { return oneVector; } }
    public static FixVec2 up { get { return upVector; } }
    public static FixVec2 down { get { return downVector; } }
    public static FixVec2 left { get { return leftVector; } }
    public static FixVec2 right { get { return rightVector; } }
    
    public Fix32 x;
    public Fix32 y;

    public Fix32 this[int index]
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

    public Fix32 magnitude
    {
        get
        {
            checked
            {
                ulong N = (ulong)((long)x.raw * x.raw + (long)y.raw * y.raw);
                return Fix32.Raw((int)(MathFix.SqrtULong(N << 2) + 1) >> 1);
            }
        }
    }

    public Fix32 sqrMagnitude
    {
        get
        {
            checked
            {
                return Fix32.Raw((int)((long)x.raw * x.raw + (long)y.raw * y.raw));
            }
        }
    }

    public FixVec2 normalized
    {
        get
        {
            if (x == 0 && y == 0)
                return zeroVector;
            Fix32 m = magnitude;
            return new FixVec2(x / m, y / m);
        }
    }

    public FixVec2(Fix32 x, Fix32 y)
    {
        this.x = x;
        this.y = y;
    }
    
    public static implicit operator FixVec2(FixVec3 v)
    {
        return new FixVec2(v.x, v.y);
    }

    public static implicit operator FixVec3(FixVec2 v)
    {
        return new FixVec3(v.x, v.y, 0);
    }

    public static FixVec2 operator +(FixVec2 rhs)
    {
        return rhs;
    }

    public static FixVec2 operator -(FixVec2 rhs)
    {
        rhs.x = -rhs.x;
        rhs.y = -rhs.y;
        return rhs;
    }

    public static FixVec2 operator +(FixVec2 lhs, FixVec2 rhs)
    {
        lhs.x += rhs.x;
        lhs.y += rhs.y;
        return lhs;
    }

    public static FixVec2 operator -(FixVec2 lhs, FixVec2 rhs)
    {
        lhs.x -= rhs.x;
        lhs.y -= rhs.y;
        return lhs;
    }

    public static FixVec2 operator +(FixVec2 lhs, Fix32 rhs)
    {
        lhs.x += rhs;
        lhs.y += rhs;
        return lhs;
    }

    public static FixVec2 operator +(Fix32 lhs, FixVec2 rhs)
    {
        rhs.x += lhs;
        rhs.y += lhs;
        return rhs;
    }

    public static FixVec2 operator -(FixVec2 lhs, Fix32 rhs)
    {
        lhs.x -= rhs;
        lhs.y -= rhs;
        return lhs;
    }

    public static FixVec2 operator *(FixVec2 lhs, FixVec2 rhs)
    {
        lhs.x *= rhs.x;
        lhs.y *= rhs.y;
        return lhs;
    }

    public static FixVec2 operator *(FixVec2 lhs, Fix32 rhs)
    {
        lhs.x *= rhs;
        lhs.y *= rhs;
        return lhs;
    }

    public static FixVec2 operator *(Fix32 lhs, FixVec2 rhs)
    {
        rhs.x *= lhs;
        rhs.y *= lhs;
        return rhs;
    }

    public static FixVec2 operator /(FixVec2 lhs, FixVec2 rhs)
    {
        lhs.x /= rhs.x;
        lhs.y /= rhs.y;
        return lhs;
    }

    public static FixVec2 operator /(FixVec2 lhs, Fix32 rhs)
    {
        lhs.x /= rhs;
        lhs.y /= rhs;
        return lhs;
    }

    public Fix32 Dot(FixVec2 rhs)
    {
        return x * rhs.x + y * rhs.y;
    }

    public Fix32 Cross(FixVec2 rhs)
    {
        return x * rhs.y - y * rhs.x;
    }

    public static bool operator ==(FixVec2 lhs, FixVec2 rhs)
    {
        return (lhs - rhs).sqrMagnitude < Fix32.Epsilon;
    }

    public static bool operator !=(FixVec2 lhs, FixVec2 rhs)
    {
        return !(lhs == rhs);
    }

    public void Set(int newX, int newY)
    {
        x = newX;
        y = newY;
    }

    public void Set(Fix32 newX, Fix32 newY)
    {
        x = newX;
        y = newY;
    }

    public static FixVec2 Lerp(FixVec2 a, FixVec2 b, Fix32 t)
    {
        t = MathFix.Clamp01(t);
        a.x += (b.x - a.x) * t;
        a.y += (b.y - a.y) * t;
        return a;
    }
    
    public static FixVec2 LerpUnclamped(FixVec2 a, FixVec2 b, Fix32 t)
    {
        a.x += (b.x - a.x) * t;
        a.y += (b.y - a.y) * t;
        return a;
    }
    
    public static FixVec2 MoveTowards(FixVec2 current, FixVec2 target, Fix32 maxDistanceDelta)
    {
        FixVec2 vec2 = target - current;
        Fix32 magnitude = vec2.magnitude;
        if (magnitude <= maxDistanceDelta || magnitude == Fix32.zero)
            return target;
        return current + vec2 / magnitude * maxDistanceDelta;
    }
    
    public static FixVec2 Scale(FixVec2 a, FixVec2 b)
    {
        a.x *= b.x;
        a.y *= b.y;
        return a;
    }
    
    public void Scale(FixVec2 scale)
    {
        x *= scale.x;
        y *= scale.y;
    }
    
    public void Normalize()
    {
        this = normalized;
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", x, y);
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode() << 2;
    }
    
    public override bool Equals(object other)
    {
        if (!(other is FixVec2))
            return false;
        return Equals((FixVec2)other);
    }

    public bool Equals(FixVec2 other)
    {
        return x == other.x && y == other.y;
    }
    
    public static FixVec2 Reflect(FixVec2 inDirection, FixVec2 inNormal)
    {
        return -2 * Dot(inNormal, inDirection) * inNormal + inDirection;
    }
    
    public static FixVec2 Perpendicular(FixVec2 inDirection)
    {
        return new FixVec2(-inDirection.y, inDirection.x);
    }
    
    public static Fix32 Dot(FixVec2 lhs, FixVec2 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y;
    }
    
    public static Fix32 Angle(FixVec2 from, FixVec2 to)
    {
        Fix32 num = MathFix.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
        if (num < Fix32.Epsilon)
            return Fix32.zero;
        return MathFix.Acos(MathFix.Clamp(Dot(from, to) / num, -Fix32.one, Fix32.one)) * MathFix.Rad2Deg;
    }
    
    public static Fix32 SignedAngle(FixVec2 from, FixVec2 to)
    {
        return Angle(from, to) * MathFix.Sign(from.x * to.y - from.y * to.x);
    }
    
    public static Fix32 Distance(FixVec2 a, FixVec2 b)
    {
        return (a - b).magnitude;
    }
    
    public static FixVec2 ClampMagnitude(FixVec2 vector, Fix32 maxLength)
    {
        if (vector.sqrMagnitude > maxLength * maxLength)
            return vector.normalized * maxLength;
        return vector;
    }

    public static Fix32 SqrMagnitude(FixVec2 a)
    {
        return a.x * a.x + a.y * a.y;
    }

    public Fix32 SqrMagnitude()
    {
        return x * x + y * y;
    }
    
    public static FixVec2 Min(FixVec2 lhs, FixVec2 rhs)
    {
        return new FixVec2(MathFix.Min(lhs.x, rhs.x), MathFix.Min(lhs.y, rhs.y));
    }
    
    public static FixVec2 Max(FixVec2 lhs, FixVec2 rhs)
    {
        return new FixVec2(MathFix.Max(lhs.x, rhs.x), MathFix.Max(lhs.y, rhs.y));
    }

    public static FixVec2 SmoothDamp(FixVec2 current, FixVec2 target, ref FixVec2 currentVelocity, Fix32 smoothTime, Fix32 maxSpeed, Fix32 deltaTime)
    {
        smoothTime = MathFix.Max(new Fix32(1, 10000), smoothTime);
        Fix32 num1 = 2 / smoothTime;
        Fix32 num2 = num1 * deltaTime;
        Fix32 num3 = 1 / (1 + num2 + new Fix32(48, 100) * num2 * num2 + new Fix32(234, 1000) * num2 * num2 * num2);
        FixVec2 vector = current - target;
        FixVec2 vector2_1 = target;
        Fix32 maxLength = maxSpeed * smoothTime;
        FixVec2 vector2_2 = FixVec2.ClampMagnitude(vector, maxLength);
        target = current - vector2_2;
        FixVec2 vector2_3 = (currentVelocity + num1 * vector2_2) * deltaTime;
        currentVelocity = (currentVelocity - num1 * vector2_3) * num3;
        FixVec2 vector2_4 = target + (vector2_2 + vector2_3) * num3;
        if (Dot(vector2_1 - current, vector2_4 - vector2_1) > Fix32.Epsilon)
        {
            vector2_4 = vector2_1;
            currentVelocity = (vector2_4 - vector2_1) / deltaTime;
        }
        return vector2_4;
    }
}
