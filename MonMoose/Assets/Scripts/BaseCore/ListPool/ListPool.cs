using System.Collections.Generic;

namespace MonMoose.Core
{
    public static class ListPool<T>
    {
        static ListPool()
        {
            ClassPoolManager.instance.RegisterOnFetch(typeof(List<T>), OnRelease);
        }

        public static List<T> Get()
        {
            return ClassPoolManager.instance.Fetch<List<T>>();
        }

        public static void Release(List<T> list)
        {
            ClassPoolManager.instance.Release(list);
        }

        private static void OnRelease(object obj)
        {
            (obj as List<T>).Clear();
        }
    }
}
