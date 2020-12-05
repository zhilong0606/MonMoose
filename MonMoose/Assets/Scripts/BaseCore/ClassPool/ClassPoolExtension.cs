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
    }
}
