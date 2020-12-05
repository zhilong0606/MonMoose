namespace MonMoose.Battle
{
    public class AttributeHandler
    {
        private int baseValue;
        private int totalValue;
        private int minValue;
        private int maxValue;
        private int addiation;
        private int growth;
        private int rate;
        private bool isDirty = true;

        private System.Func<int, int, int, int, int> calculator;

        public AttributeHandler(int baseValue, int minValue, int maxValue, int growth, System.Func<int, int, int, int, int> calculator)
        {
            this.baseValue = baseValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.growth = growth;
            this.calculator = calculator;
            rate = 0;
            isDirty = true;
        }

        public int BaseValue
        {
            get { return baseValue; }
        }

        public int TotalValue
        {
            get
            {
                if (isDirty)
                {
                    totalValue = calculator(baseValue, addiation, growth, rate);
                    if (minValue > totalValue)
                    {
                        totalValue = minValue;
                    }

                    if (maxValue < totalValue)
                    {
                        totalValue = maxValue;
                    }
                    isDirty = false;
                }
                return totalValue;
            }
        }

    }
}
