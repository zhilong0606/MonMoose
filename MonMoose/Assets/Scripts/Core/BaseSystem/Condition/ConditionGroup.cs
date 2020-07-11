using System;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public class ConditionGroup : BaseCondition
    {
        private ConditionValue[] values;
        public Action<bool> OnValueChanged;
        private BaseCondition rootCondition;
        private List<BaseCondition> cachedList = new List<BaseCondition>();
        private List<BaseCondition> checkDupList = new List<BaseCondition>();

        public ConditionValue this[int idx]
        {
            get
            {
                if (idx >= 0 && idx < values.Length)
                {
                    return values[idx];
                }
                return null;
            }
        }

        public override bool IsDirty
        {
            get
            {
                if (rootCondition != null)
                {
                    return rootCondition.IsDirty;
                }
                return false;
            }
        }

        public void Resize(int count)
        {
            Clear();
            values = new ConditionValue[count];
            for (int i = 0; i < values.Length; ++i)
            {
                values[i] = NewValue();
            }
        }

        public void ClearCache()
        {
            for (int i = 0; i < cachedList.Count; ++i)
            {
                cachedList[i].Release();
            }
            cachedList.Clear();
            rootCondition = null;
        }

        public void SetRoot(BaseCondition root)
        {
            if (root == rootCondition)
            {
                return;
            }
            ClearCache();
            if (root != null)
            {
                rootCondition = root;
                for (int i = 0; i < values.Length; ++i)
                {
                    BaseCondition c = values[i].Parent;
                    checkDupList.Clear();
                    while (c != null)
                    {
                        if (checkDupList.Contains(c))
                        {
                            DebugUtility.LogError("Error : Condition is Duplicated!!!!");
                            break;
                        }
                        if (cachedList.Contains(c))
                        {
                            break;
                        }
                        cachedList.Add(c);
                        checkDupList.Add(c);
                        c = c.Parent;
                    }
                }
            }

        }

        public void Clear()
        {
            if (values != null)
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    values[i].Release();
                    values[i] = null;
                }
            }
            values = null;
            OnValueChanged = null;
            ClearCache();
        }

        public override void Reset()
        {
            Clear();
            base.Reset();
        }

        private void OnConditionValueChanged()
        {
            Check();
        }

        public void Check()
        {
            CalcResult();
        }

        protected override void CalcResult()
        {
            bool needCb = false;
            if (IsDirty)
            {
                bool r = rootCondition.GetResult();
                if (r != result)
                {
                    needCb = true;
                    result = r;
                }
            }
            if (needCb && OnValueChanged != null)
            {
                OnValueChanged(result);
            }
        }

        private ConditionValue NewValue()
        {
            ConditionValue value = ClassPoolManager.instance.Fetch<ConditionValue>();
            InitValue(value);
            return value;
        }

        private void InitValue(ConditionValue value)
        {
            value.OnValueChanged = OnConditionValueChanged;
        }
    }
}