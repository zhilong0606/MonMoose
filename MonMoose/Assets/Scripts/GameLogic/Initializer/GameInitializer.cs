using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.Core;
using UnityEngine;

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
        GameDataManager.CreateInstance();
        FrameSyncManager.CreateInstance();
        yield return null;
    }
}
