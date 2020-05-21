namespace MonMoose.Core
{
    public interface IUIProcess
    {
        bool needSkip { get; }
        void StartProcess();
    }
}
