namespace MonMoose.Core
{
    public abstract class ClassPoolObj : IClassPoolObj
    {
        public ClassPool creater { get; set; }
        public object causer { get; set; }

        public virtual void OnFetch()
        {
        }

        public virtual void OnRelease()
        {
        }
    }
}
