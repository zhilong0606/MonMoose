using UnityEngine;

namespace MonMoose.Logic
{
    public static class FrameSyncExtension
    {
        public static Vector3 ToVector3(this FixVec3 v)
        {
            return new Vector3((float)v.x, (float)v.y, (float)v.z);
        }
    }
}
