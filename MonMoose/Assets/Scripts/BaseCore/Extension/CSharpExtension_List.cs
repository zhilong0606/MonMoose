using System;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public static class CSharpExtension_List
    {
        public static bool TryGetValue<T>(this IList<T> l, int index, out T value)
        {
            if (index >= 0 && index < l.Count)
            {
                value = l[index];
                return true;
            }
            value = default(T);
            return false;
        }

        public static T GetValueSafely<T>(this IList<T> list, int index, T defaultValue = default(T))
        {
            if (list != null && index >= 0 && index < list.Count)
            {
                return list[index];
            }
            return defaultValue;
        }

        public static T GetValueSafelyByLast<T>(this IList<T> list, int index, T defaultValue = default(T))
        {
            if (list != null && list.Count > 0)
            {
                return GetValueSafely(list, index, list[list.Count - 1]);
            }
            return defaultValue;
        }
    }
}
