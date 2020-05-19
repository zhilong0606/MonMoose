using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateAttributeValue : AttributeValue
{
    public override EType Type
    {
        get { return EType.Rate; }
    }

    public override Fix32 Calculate(Fix32 f)
    {
        return f * (1 + m_value * new Fix32(1, 100));
    }
}
