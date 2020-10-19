using System;

namespace MonMoose.Core
{
    public static class ClassPoolExtension
    {
        public static void Release(this IClassPoolObj obj)
        {
            if (obj != null && obj.creater != null)
            {
                obj.creater.Release(obj);
            }
        }

        public static T Fetch<T>(this object obj) where T : class
        {
            return ClassPoolManager.instance.Fetch<T>();
        }
    }

    public static class ClassPoolCut<T> where T : class
    {
        public static T Fetch()
        {
            return ClassPoolManager.instance.Fetch<T>();
        }
    }
}
