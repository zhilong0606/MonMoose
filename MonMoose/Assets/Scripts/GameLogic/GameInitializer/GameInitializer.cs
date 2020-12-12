using System.Collections;
using MonMoose.Core;

namespace MonMoose.GameLogic
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
            yield return null;
        }
    }
}
