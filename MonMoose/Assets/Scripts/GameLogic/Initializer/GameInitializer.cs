using System.Collections;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public class GameInitializer : Initializer
    {
        public GameInitializer()
        {
            AddSubInitializer(new StaticDataInitializer());
            AddSubInitializer(new SettingDefineInitializer());
            AddSubInitializer(new TickRegisterInitializer());
            AddSubInitializer(new UIWindowDefineInitializer());
        }

        protected override IEnumerator OnProcess()
        {
            EventManager.CreateInstance();
            UIWindowManager.CreateInstance();
            ResourceManager.CreateInstance();
            TimerManager.CreateInstance();
            FrameSyncManager.CreateInstance();
            yield return null;
        }
    }
}
