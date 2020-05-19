﻿using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

public enum EWindowType
{
    None,
    GameInitWindow,
    LobbyWindow,
}

public class UIWindowDefineInitializer : Initializer
{
    public UIWindowDefineInitializer()
    {
    }

    protected override IEnumerator OnProcess()
    {
        UIWindowManager.instance.RegisterWindowContext((int)EWindowType.GameInitWindow, new UIWindowContext("UI/GameInit/Prefabs/GameInitWindow", typeof(GameInitWindow)));

        UIWindowManager.instance.RegisterWindowContext((int)EWindowType.LobbyWindow, new UIWindowContext("Exported/UI/Lobby/Prefabs/LobbyWindow", typeof(LobbyWindow)));
        yield return null;
    }
}
