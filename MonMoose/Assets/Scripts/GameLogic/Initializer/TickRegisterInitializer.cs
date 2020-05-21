using System.Collections;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public class TickRegisterInitializer : Initializer
    {
        protected override IEnumerator OnProcess()
        {
            TickManager.instance.RegisterGlobalTick(FrameSyncManager.instance.Tick);
            yield return null;
        }
    }
}