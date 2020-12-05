using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.StaticData;

namespace MonMoose.Logic
{
    public class AttributeInfo : AbstractObserver
    {
        public ConstAttributeValue baseValue;
        public ConstAttributeValue extraValue;
        public List<RateAttributeValue> rateValueList = new List<RateAttributeValue>();
        public List<ConstAttributeValue> constValueList = new List<ConstAttributeValue>();
        public AttributeStaticInfo staticInfo;

        public AttributeInfo()
        {
            baseValue = ClassPoolManager.instance.Fetch<ConstAttributeValue>(this);
            extraValue = ClassPoolManager.instance.Fetch<ConstAttributeValue>(this);
            AddSubject(baseValue);
            AddSubject(extraValue);
        }

        public Dcm32 totalValue
        {
            get
            {
                Dcm32 ret = baseValue.Value;
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
}
