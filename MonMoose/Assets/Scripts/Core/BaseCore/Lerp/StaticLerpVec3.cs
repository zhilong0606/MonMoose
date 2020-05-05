using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Core
{
    public class StaticLerpVec3 : AbstractStaticLerp<Vector3>
    {
        //private StaticLerpFloat x = new StaticLerpFloat();
        //private StaticLerpFloat y = new StaticLerpFloat();
        //private StaticLerpFloat z = new StaticLerpFloat();

        //public Vector3 CurValue
        //{
        //    get { return new Vector3(x.CurValue, y.CurValue, z.CurValue); }
        //    set
        //    {
        //        x.CurValue = value.x;
        //        y.CurValue = value.y;
        //        z.CurValue = value.z;
        //    }
        //}

        //public Vector3 PreValue
        //{
        //    get { return new Vector3(x.PreValue, y.PreValue, z.PreValue); }
        //}

        //public Vector3 TargetValue
        //{
        //    get { return new Vector3(x.TargetValue, y.TargetValue, z.TargetValue); }
        //}

        //public float CurTime
        //{
        //    get { return (x.CurTime + y.CurTime + z.CurTime) / 3f; }
        //}

        //public bool IsStart
        //{
        //    get { return x.IsStart && y.IsStart && x.IsStart; }
        //}

        //public void Start(EStaticLerpMode lerpMode, Vector3 startValue, Vector3 targetValue, float time)
        //{
        //    x.Start(lerpMode, startValue.x, targetValue.x, time);
        //    y.Start(lerpMode, startValue.y, targetValue.y, time);
        //    z.Start(lerpMode, startValue.z, targetValue.z, time);
        //}

        //public void Ready(EStaticLerpMode lerpMode, Vector3 startValue, Vector3 targetValue, float time)
        //{
        //    x.Ready(lerpMode, startValue.x, targetValue.x, time);
        //    y.Ready(lerpMode, startValue.y, targetValue.y, time);
        //    z.Ready(lerpMode, startValue.z, targetValue.z, time);
        //}

        //public void Stop()
        //{
        //    x.Stop();
        //    y.Stop();
        //    z.Stop();
        //}

        //public void ManualUpdate(Vector3 targetValue, float time)
        //{
        //    x.ManualUpdate(targetValue.x, time);
        //    y.ManualUpdate(targetValue.y, time);
        //    z.ManualUpdate(targetValue.z, time);
        //}

        //public void Update(float deltaFloat)
        //{
        //    x.Update(deltaFloat);
        //    y.Update(deltaFloat);
        //    z.Update(deltaFloat);
        //}
        protected override Vector3 Lerp(Vector3 start, Vector3 end, float f)
        {
            return start + (end - start) * f;
        }
    }
}