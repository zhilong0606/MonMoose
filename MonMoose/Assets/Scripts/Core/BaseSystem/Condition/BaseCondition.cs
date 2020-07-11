namespace MonMoose.Core
{
    public class BaseCondition : ClassPoolObj
    {
        protected bool isDirty;
        protected bool result;
        protected BaseCondition parent;

        public virtual bool IsDirty
        {
            get { return isDirty; }
        }

        public BaseCondition Parent
        {
            get { return parent; }
        }

        public bool GetResult()
        {
            if (IsDirty)
            {
                CalcResult();
            }
            return result;
        }

        protected virtual void CalcResult()
        {
            isDirty = false;
        }

        public new virtual void Reset()
        {
            isDirty = false;
            result = false;
            parent = null;
        }

        public sealed override void OnRelease()
        {
            Reset();
        }

        protected void Attach(BaseCondition child)
        {
            child.parent = this;
        }

        public static OrCondition operator |(BaseCondition c1, BaseCondition c2)
        {
            OrCondition c = ClassPoolManager.instance.Fetch<OrCondition>();
            c.Init(c1, c2);
            return c;
        }

        public static AndCondition operator &(BaseCondition c1, BaseCondition c2)
        {
            AndCondition c = ClassPoolManager.instance.Fetch<AndCondition>();
            c.Init(c1, c2);
            return c;
        }

        public static NotCondition operator !(BaseCondition c1)
        {
            NotCondition c = ClassPoolManager.instance.Fetch<NotCondition>();
            c.Init(c1);
            return c;
        }
    }
}
