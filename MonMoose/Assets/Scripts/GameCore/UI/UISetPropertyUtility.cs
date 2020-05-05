using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UISetPropertyUtility
{
    public static bool SetColor(ref Color currentValue, Color newValue)
    {
        if (Math.Abs(currentValue.r - newValue.r) < CommonDefine.FloatEpsilon
            && Math.Abs(currentValue.g - newValue.g) < CommonDefine.FloatEpsilon
            && Math.Abs(currentValue.b - newValue.b) < CommonDefine.FloatEpsilon
            && Math.Abs(currentValue.a - newValue.a) < CommonDefine.FloatEpsilon)
        {
            return false;
        }
        currentValue = newValue;
        return true;
    }

    public static bool SetEquatableStruct<T>(ref T currentValue, T newValue) where T : IEquatable<T>
    {
        if (currentValue.Equals(newValue))
        {
            return false;
        }
        currentValue = newValue;
        return true;
    }

    public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
    {
        if (currentValue.Equals(newValue))
        {
            return false;
        }
        currentValue = newValue;
        return true;
    }

    public static bool SetStructArray<T>(ref T[] currentValue, T[] newValue) where T : struct
    {
        if (currentValue.Equals(newValue))
        {
            return false;
        }
        currentValue = newValue;
        return true;
    }

    public static bool SetClassArray<T>(ref T[] currentValue, T[] newValue) where T : class
    {
        if ((currentValue == null && newValue == null)
            || (currentValue != null && currentValue.Equals(newValue)))
        {
            return false;
        }
        currentValue = newValue;
        return true;
    }

    public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
    {
        if ((currentValue == null && newValue == null)
            || (currentValue != null && currentValue.Equals(newValue)))
        {
            return false;
        }
        currentValue = newValue;
        return true;
    }
}
