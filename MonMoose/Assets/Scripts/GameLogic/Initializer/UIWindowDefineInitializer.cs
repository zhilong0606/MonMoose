using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

public enum EWindowType
{
    None,
    JoystickWindow,
    LobbyWindow,
}

public class UIWindowDefineInitializer : Initializer
{
    public UIWindowDefineInitializer()
    {
    }

    protected override IEnumerator OnProcess()
    {
        UIWindowManager.instance.RegisterWindowContext((int)EWindowType.LobbyWindow, new UIWindowContext("Exported/UI/Lobby/Prefabs/LobbyPanel", typeof(LobbyPanel)));
        yield return null;
    }
}
