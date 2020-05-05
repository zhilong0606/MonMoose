
using System;
using UnityEngine;

public struct FixVec3 : IEquatable<FixVec3>
{
    public static readonly FixVec3 zeroVector = new FixVec3();
    public static readonly FixVec3 oneVector = new FixVec3(1, 1, 1);
    public static readonly FixVec3 upVector = new FixVec3(0, 1, 0);
    public static readonly FixVec3 downVector = new FixVec3(0, -1, 0);
    public static readonly FixVec3 leftVector = new FixVec3(-1, 0, 0);
    public static readonly FixVec3 rightVector = new FixVec3(1, 0, 0);
    public static readonly FixVec3 forwardVector = new FixVec3(0, 0, 1);
    public static readonly FixVec3 backVector = new FixVec3(0, 0, -1);

    public static FixVec3 zero { get { return zeroVector; } }
    public static FixVec3 one { get { return oneVector; } }
    public static FixVec3 up { get { return upVector; } }
    public static FixVec3 down { get { return downVector; } }
    public static FixVec3 left { get { return leftVector; } }
    public static FixVec3 right { get { return rightVector; } }
    public static FixVec3 forward { get { return forwardVector; } }
    public static FixVec3 back { get { return backVector; } }
    
    public Fix32 x;
    public Fix32 y;
    public Fix32 z;

    public Fix32 this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return x;
                case 1:
                    return y;
                case 2:
                    return z;
                default:
                    throw new IndexOutOfRangeException("Invalid Vector3 index!");
            }
        }
        set
        {
            switch (index)
            {
                case 0:
                    x = value;
                    break;
                case 1:
                    y = value;
                    break;
                case 2:
                    z = value;
                    break;
                default:
                    throw new IndexOutOfRangeException("Invalid Vector3 index!");
            }
        }
    }

    public FixVec3 normalized
    {
        get
        {
            if (x == 0 && y == 0 && z == 0)
                return zero;

            var m = magnitude;
            return new FixVec3(x / m, y / m, z / m);
        }
    }

    public Fix32 magnitude
    {
        get
        {
            checked
            {
                ulong N = (ulong)((long)x.raw * x.raw + (long)y.raw * y.raw + (long)z.raw * z.raw);
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
                return Fix32.Raw((int)((long)x.raw * x.raw + (long)y.raw * y.raw + (long)z.raw * z.raw));
            }
        }
    }

    public FixVec3(Fix32 x, Fix32 y, Fix32 z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public FixVec3(Fix32 x, Fix32 y)
    {
        this.x = x;
        this.y = y;
        this.z = 0;
    }

    public static implicit operator FixVec2(FixVec3 v)
    {
        return new FixVec2(v.x, v.y);
    }

    public static implicit operator FixVec3(FixVec2 v)
    {
        return new FixVec3(v.x, v.y, 0);
    }

    public static implicit operator Vector3(FixVec3 v)
    {
        return new Vector3((float)v.x, (float)v.y, (float)v.z);
    }

    public static implicit operator FixVec3(Vector3 v)
    {
        return new FixVec3(v.x, v.y, v.z);
    }

    public static FixVec3 operator +(FixVec3 rhs)
    {
        return rhs;
    }

    public static FixVec3 operator -(FixVec3 rhs)
    {
        rhs.x = -rhs.x;
        rhs.y = -rhs.y;
        rhs.z = -rhs.z;
        return rhs;
    }

    public static FixVec3 operator +(FixVec3 lhs, FixVec3 rhs)
    {
        lhs.x += rhs.x;
        lhs.y += rhs.y;
        lhs.z += rhs.z;
        return lhs;
    }

    public static FixVec3 operator -(FixVec3 lhs, FixVec3 rhs)
    {
        lhs.x -= rhs.x;
        lhs.y -= rhs.y;
        lhs.z -= rhs.z;
        return lhs;
    }

    public static FixVec3 operator +(FixVec3 lhs, Fix32 rhs)
    {
        lhs.x += rhs;
        lhs.y += rhs;
        lhs.z += rhs;
        return lhs;
    }

    public static FixVec3 operator +(Fix32 lhs, FixVec3 rhs)
    {
        rhs.x += lhs;
        rhs.y += lhs;
        rhs.z += lhs;
        return rhs;
    }

    public static FixVec3 operator -(FixVec3 lhs, Fix32 rhs)
    {
        lhs.x -= rhs;
        lhs.y -= rhs;
        lhs.z -= rhs;
        return lhs;
    }

    public static FixVec3 operator *(FixVec3 lhs, FixVec3 rhs)
    {
        lhs.x *= rhs.x;
        lhs.y *= rhs.y;
        lhs.z *= rhs.z;
        return lhs;
    }

    public static FixVec3 operator *(FixVec3 lhs, Fix32 rhs)
    {
        lhs.x *= rhs;
        lhs.y *= rhs;
        lhs.z *= rhs;
        return lhs;
    }

    public static FixVec3 operator *(Fix32 lhs, FixVec3 rhs)
    {
        rhs.x *= lhs;
        rhs.y *= lhs;
        rhs.z *= lhs;
        return rhs;
    }

    public static FixVec3 operator /(FixVec3 lhs, FixVec3 rhs)
    {
        lhs.x /= rhs.x;
        lhs.y /= rhs.y;
        lhs.z /= rhs.z;
        return lhs;
    }

    public static FixVec3 operator /(FixVec3 lhs, Fix32 rhs)
    {
        lhs.x /= rhs;
        lhs.y /= rhs;
        lhs.z /= rhs;
        return lhs;
    }

    public static bool operator ==(FixVec3 lhs, FixVec3 rhs)
    {
        return (lhs - rhs).sqrMagnitude < Fix32.Epsilon;
    }

    public static bool operator !=(FixVec3 lhs, FixVec3 rhs)
    {
        return !(lhs == rhs);
    }

    public void Set(int newX, int newY, int newZ)
    {
        x = newX;
        y = newY;
        z = newZ;
    }

    public void Set(Fix32 newX, Fix32 newY, Fix32 newZ)
    {
        x = newX;
        y = newY;
        z = newZ;
    }

    public static FixVec3 Lerp(FixVec3 a, FixVec3 b, Fix32 t)
    {
        t = MathFix.Clamp01(t);
        a.x += (b.x - a.x) * t;
        a.y += (b.y - a.y) * t;
        a.z += (b.z - a.z) * t;
        return a;
    }

    public static FixVec3 LerpUnclamped(FixVec3 a, FixVec3 b, Fix32 t)
    {
        a.x += (b.x - a.x) * t;
        a.y += (b.y - a.y) * t;
        a.z += (b.z - a.z) * t;
        return a;
    }

    public static FixVec3 MoveTowards(FixVec3 current, FixVec3 target, Fix32 maxDistanceDelta)
    {
        FixVec3 vec3 = target - current;
        Fix32 magnitude = vec3.magnitude;
        if (magnitude <= maxDistanceDelta || magnitude == Fix32.zero)
            return target;
        return current + vec3 / magnitude * maxDistanceDelta;
    }

    public static FixVec3 Scale(FixVec3 a, FixVec3 b)
    {
        a.x *= b.x;
        a.y *= b.y;
        a.z *= b.z;
        return a;
    }

    public void Scale(FixVec3 scale)
    {
        x *= scale.x;
        y *= scale.y;
        z *= scale.z;
    }

    public void Normalize()
    {
        this = normalized;
    }

    public override string ToString()
    {
        return string.Format("({0}, {1}, {2})", x, y, z);
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2;
    }

    public override bool Equals(object other)
    {
        if (!(other is FixVec3))
            return false;
        return Equals((FixVec3)other);
    }

    public bool Equals(FixVec3 other)
    {
        return x == other.x && y == other.y && z == other.z;
    }

    public Fix32 Dot(FixVec3 rhs)
    {
        return x * rhs.x + y * rhs.y + z * rhs.z;
    }

    public FixVec3 Cross(FixVec3 rhs)
    {
        return new FixVec3(
            y * rhs.z - z * rhs.y,
            z * rhs.x - x * rhs.z,
            x * rhs.y - y * rhs.x
            );
    }

    public static FixVec3 Reflect(FixVec3 inDirection, FixVec3 inNormal)
    {
        return -2 * Dot(inNormal, inDirection) * inNormal + inDirection;
    }

    public static Fix32 Dot(FixVec3 lhs, FixVec3 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y +lhs.z * rhs.z;
    }

    public static FixVec3 Cross(FixVec3 lhs, FixVec3 rhs)
    {
        return new FixVec3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
    }

    public static Fix32 Angle(FixVec3 from, FixVec3 to)
    {
        Fix32 num = MathFix.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
        if (num < Fix32.Epsilon)
            return Fix32.zero;
        return MathFix.Acos(MathFix.Clamp(Dot(from, to) / num, -Fix32.one, Fix32.one)) * MathFix.Rad2Deg;
    }

    public static Fix32 SignedAngle(FixVec3 from, FixVec3 to, FixVec3 axis)
    {
        return Angle(from, to) * MathFix.Sign(Dot(axis, Cross(from, to)));
    }

    public static Fix32 Distance(FixVec3 a, FixVec3 b)
    {
        return (a - b).magnitude;
    }

    public static FixVec3 ClampMagnitude(FixVec3 vector, Fix32 maxLength)
    {
        if (vector.sqrMagnitude > maxLength * maxLength)
            return vector.normalized * maxLength;
        return vector;
    }

    public static Fix32 SqrMagnitude(FixVec3 a)
    {
        return a.x * a.x + a.y * a.y + a.z * a.z;
    }

    public Fix32 SqrMagnitude()
    {
        return x * x + y * y + z * z;
    }

    public static FixVec3 Min(FixVec3 lhs, FixVec3 rhs)
    {
        return new FixVec3(MathFix.Min(lhs.x, rhs.x), MathFix.Min(lhs.y, rhs.y), MathFix.Min(lhs.z, rhs.z));
    }

    public static FixVec3 Max(FixVec3 lhs, FixVec3 rhs)
    {
        return new FixVec3(MathFix.Max(lhs.x, rhs.x), MathFix.Max(lhs.y, rhs.y), MathFix.Max(lhs.z, rhs.z));
    }

    public static FixVec3 SmoothDamp(FixVec3 current, FixVec3 target, ref FixVec3 currentVelocity, Fix32 smoothTime, Fix32 maxSpeed, Fix32 deltaTime)
    {
        smoothTime = MathFix.Max(new Fix32(1, 10000), smoothTime);
        Fix32 num1 = 2 / smoothTime;
        Fix32 num2 = num1 * deltaTime;
        Fix32 num3 = 1 / (1 + num2 + new Fix32(48, 100) * num2 * num2 + new Fix32(234, 1000) * num2 * num2 * num2);
        FixVec3 vector = current - target;
        FixVec3 vector2_1 = target;
        Fix32 maxLength = maxSpeed * smoothTime;
        FixVec3 vector2_2 = FixVec3.ClampMagnitude(vector, maxLength);
        target = current - vector2_2;
        FixVec3 vector2_3 = (currentVelocity + num1 * vector2_2) * deltaTime;
        currentVelocity = (currentVelocity - num1 * vector2_3) * num3;
        FixVec3 vector2_4 = target + (vector2_2 + vector2_3) * num3;
        if (Dot(vector2_1 - current, vector2_4 - vector2_1) > Fix32.Epsilon)
        {
            vector2_4 = vector2_1;
            currentVelocity = (vector2_4 - vector2_1) / deltaTime;
        }
        return vector2_4;
    }

    
    





}
