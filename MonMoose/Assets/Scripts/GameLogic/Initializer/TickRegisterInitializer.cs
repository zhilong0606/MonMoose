using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

public class TickRegisterInitializer : Initializer
{
    protected override IEnumerator OnProcess()
    {
        TickManager.instance.RegisterGlobalTick(FrameSyncManager.instance.Tick);
        yield return null;
    }
}
