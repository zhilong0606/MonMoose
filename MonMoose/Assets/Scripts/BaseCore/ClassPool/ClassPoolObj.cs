namespace MonMoose.Core
{
    public class ClassPoolObj : IClassPoolObj
    {
        public ClassPool creater { get; set; }

        public virtual void OnFetch()
        {
        }

        public virtual void OnRelease()
        {
        }
    }
}
