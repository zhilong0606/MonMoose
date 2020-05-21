using System.Collections;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public enum EWindowType
    {
        None,
        GameInitWindow,
        LobbyWindow,
    }

    public static class UIWindowDefine
    {
        public static void Define()
        {
            UIWindowManager.instance.RegisterWindowContext((int)EWindowType.GameInitWindow, new UIWindowContext("UI/GameInit/Prefabs/GameInitWindow", typeof(GameInitWindow)));

            UIWindowManager.instance.RegisterWindowContext((int)EWindowType.LobbyWindow, new UIWindowContext("Exported/UI/Lobby/Prefabs/LobbyWindow", typeof(LobbyWindow)));
        }
    }
}
