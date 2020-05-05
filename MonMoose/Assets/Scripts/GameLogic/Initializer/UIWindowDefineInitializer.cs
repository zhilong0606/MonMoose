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
        AddSubInitializer(new JoystickDefineInitializer());
    }

    protected override IEnumerator OnProcess()
    {
        UIWindowManager.instance.RegisterWindowContext((int)EWindowType.JoystickWindow, new UIWindowContext("Exporter/UI/Joystick/Prefabs/JoystickPanel", typeof(JoystickPanel)));
        UIWindowManager.instance.RegisterWindowContext((int)EWindowType.LobbyWindow, new UIWindowContext("Exporter/UI/Lobby/Prefabs/LobbyPanel", typeof(LobbyPanel)));
        yield return null;
    }
}
