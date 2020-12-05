namespace MonMoose.Core
{
    public interface IClassPoolObj
    {
        object causer { get; set; }
        ClassPool creater { get; set; }
        void OnFetch();
        void OnRelease();
    }
}