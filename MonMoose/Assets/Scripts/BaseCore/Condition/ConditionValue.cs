using System;

namespace MonMoose.Core
{
    public class ConditionValue : BaseCondition
    {
        public Action OnValueChanged;

        public bool Value
        {
            get { return result; }
            set
            {
                if (value != result)
                {
                    isDirty = true;
                    result = value;
                    OnValueChanged();
                }
            }
        }
    }
}