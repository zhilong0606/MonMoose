namespace MonMoose.Logic.Battle
{
    public class PoolObj
    {
        public Pool creater { get; set; }

        public virtual void OnFetch()
        {
        }

        public virtual void OnRelease()
        {
        }

        public void Release()
        {
            creater.Release(this);
        }
    }
}
