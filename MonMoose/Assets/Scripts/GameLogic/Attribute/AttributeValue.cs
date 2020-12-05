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

        protected Dcm32 m_value;
        public abstract EType Type { get; }
        public ClassPool creater { get; set; }
        public object causer { get; set; }

        public bool IsZero
        {
            get { return MathDcm.Abs(m_value - Dcm32.zero) < Dcm32.Epsilon; }
        }

        public abstract Dcm32 Calculate(Dcm32 f);

        public Dcm32 Value
        {
            get { return m_value; }
            set
            {
                if (MathDcm.Abs(m_value - value) > Dcm32.Epsilon)
                {
                    m_value = value;
                    Notify((int)ENotifyId.ValueChanged);
                }
            }
        }

        public virtual void OnFetch()
        {
            m_value = Dcm32.zero;
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
