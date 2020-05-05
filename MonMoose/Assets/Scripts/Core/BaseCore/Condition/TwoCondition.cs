namespace MonMoose.Core
{
    public class TwoCondition : BaseCondition
    {
        protected BaseCondition condition1;
        protected BaseCondition condition2;

        public override bool IsDirty
        {
            get { return condition1.IsDirty || condition2.IsDirty; }
        }

        public void Init(BaseCondition c1, BaseCondition c2)
        {
            condition1 = c1;
            condition2 = c2;
            Attach(c1);
            Attach(c2);
        }

        public override void Reset()
        {
            condition1 = null;
            condition2 = null;
            base.Reset();
        }
    }
}