namespace MonMoose.Core
{
    public interface IClassPoolObj
    {
        ClassPool creater { get; set; }
        void OnFetch();
        void OnRelease();
    }
}