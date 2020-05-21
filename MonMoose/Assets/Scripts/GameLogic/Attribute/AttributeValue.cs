using MonMoose.Core;

namespace MonMoose.Logic
{
    public abstract class AttributeValue : AbstractObserveSubject, IClassPoolObj
    {
        public enum EType
        {
            Const,
            Rate,
        }

        protected Fix32 m_value;
        public abstract EType Type { get; }
        public ClassPool creater { get; set; }

        public bool IsZero
        {
            get { return MathFix.Abs(m_value - Fix32.zero) < Fix32.Epsilon; }
        }

        public abstract Fix32 Calculate(Fix32 f);

        public Fix32 Value
        {
            get { return m_value; }
            set
            {
                if (MathFix.Abs(m_value - value) > Fix32.Epsilon)
                {
                    m_value = value;
                    Notify((int)ENotifyId.ValueChanged);
                }
            }
        }

        public virtual void OnFetch()
        {
            m_value = Fix32.zero;
        }

        public virtual void OnRelease()
        {
            m_observerList.Clear();
        }

        public enum ENotifyId
        {
            ValueChanged,
        }
    }
}
