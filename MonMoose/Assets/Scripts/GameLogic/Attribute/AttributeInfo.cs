using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.StaticData;
using UnityEngine;

public class AttributeInfo : AbstractObserver
{
    public ConstAttributeValue baseValue;
    public ConstAttributeValue extraValue;
    public List<RateAttributeValue> rateValueList = new List<RateAttributeValue>();
    public List<ConstAttributeValue> constValueList = new List<ConstAttributeValue>();
    public AttributeStaticInfo staticInfo;

    public AttributeInfo()
    {
        baseValue = ClassPoolManager.instance.Fetch<ConstAttributeValue>();
        extraValue = ClassPoolManager.instance.Fetch<ConstAttributeValue>();
        AddSubject(baseValue);
        AddSubject(extraValue);
    }

    public Fix32 totalValue
    {
        get
        {
            Fix32 ret = baseValue.Value;
            ret += extraValue.Value;
            //for (int i = 0; i < rateValueList.Count; ++i)
            //{
            //    rateValueList[i].Calculate()
            //}
            return ret;
        }
    }

    public override void OnReceive(int eventId, IObserveSubject subject)
    {

    }
}
