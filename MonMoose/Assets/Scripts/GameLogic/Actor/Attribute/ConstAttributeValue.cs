using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstAttributeValue : AttributeValue
{
    public override EType Type
    {
        get { return EType.Const; }
    }

    public override Fix32 Calculate(Fix32 f)
    {
        return f + m_value;
    }
}
