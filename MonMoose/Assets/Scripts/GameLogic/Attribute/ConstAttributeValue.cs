namespace MonMoose.Logic
{
    public class ConstAttributeValue : AttributeValue
    {
        public override EType Type
        {
            get { return EType.Const; }
        }

        public override Dcm32 Calculate(Dcm32 f)
        {
            return f + m_value;
        }
    }
}
