namespace MonMoose.Core
{
    public class AndCondition : TwoCondition
    {
        protected override void CalcResult()
        {
            bool r1 = condition1.GetResult();
            bool r2 = condition2.GetResult();
            result = r1 && r2;
            base.CalcResult();
        }
    }
}