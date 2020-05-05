using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonMoose.Core;

public class CurveLerpFunc : BaseScriptableObject, ILerpFunc
{
    public AnimationCurve m_curve = new AnimationCurve();

    public float GetValue(float f)
    {
        return m_curve.Evaluate(f);
    }
}
