using System.Collections;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public class GameInitializer : Initializer
    {
        public GameInitializer()
        {
            AddSubInitializer(new UIWindowDefineInitializer());
            AddSubInitializer(new StaticDataInitializer());
            AddSubInitializer(new SettingDefineInitializer());
        }

        protected override IEnumerator OnProcess()
        {
            EventManager.CreateInstance();
            UIWindowManager.CreateInstance();
            ResourceManager.CreateInstance();
            TimerManager.CreateInstance();
            yield return null;
        }
    }
}
