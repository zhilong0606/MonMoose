namespace MonMoose.Core
{
    public class NotCondition : BaseCondition
    {
        private BaseCondition condition;

        public override bool IsDirty
        {
            get { return condition.IsDirty; }
        }

        public void Init(BaseCondition c)
        {
            condition = c;
            Attach(c);
        }

        protected override void CalcResult()
        {
            result = !condition.GetResult();
            base.CalcResult();
        }

        public override void Reset()
        {
            condition = null;
            base.Reset();
        }
    }
}