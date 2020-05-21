using System.Collections;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public class GameInitializer : Initializer
    {
        public GameInitializer()
        {
            AddSubInitializer(new StaticDataInitializer());
            AddSubInitializer(new ResourceInitializer());
        }

        protected override IEnumerator OnProcess()
        {
            UIWindowManager.instance.OpenWindow((int)EWindowId.LoadingWindow);
            yield return null;
        }
    }
}
