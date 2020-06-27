namespace MonMoose.Logic
{
    public class RateAttributeValue : AttributeValue
    {
        public override EType Type
        {
            get { return EType.Rate; }
        }

        public override Dcm32 Calculate(Dcm32 f)
        {
            return f * (1 + m_value * new Dcm32(1, 100));
        }
    }
}
