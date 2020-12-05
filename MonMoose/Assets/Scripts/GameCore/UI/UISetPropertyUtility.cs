using System;
using UnityEngine;

namespace MonMoose.Core
{
    public static class UISetPropertyUtility
    {
        public static bool SetColor(ref Color currentValue, Color newValue)
        {
            if (currentValue.r.IsEqualTo(newValue.r)
                && currentValue.g.IsEqualTo(newValue.g)
                && currentValue.b.IsEqualTo(newValue.b)
                && currentValue.a.IsEqualTo(newValue.a))
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
}
