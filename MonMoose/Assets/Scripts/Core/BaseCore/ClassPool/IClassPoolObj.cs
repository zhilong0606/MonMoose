namespace MonMoose.Core
{
    public interface IClassPoolObj
    {
#if !RELEASE
        object causer { get; set; }
#endif
        ClassPool creater { get; set; }
        void OnFetch();
        void OnRelease();
    }
}