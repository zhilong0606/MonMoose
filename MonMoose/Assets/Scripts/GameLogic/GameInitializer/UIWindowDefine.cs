using System.Collections;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public enum EWindowId
    {
        None,
        GameInitWindow,
        LobbyWindow,
        LoadingWindow,
    }

    public static class UIWindowDefine
    {
        public static void Define()
        {
            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.GameInitWindow, new UIWindowContext("UI/GameInit/Prefabs/GameInitWindow", typeof(GameInitWindow)));

            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.LobbyWindow, new UIWindowContext("Exported/UI/Lobby/Prefabs/LobbyWindow", typeof(LobbyWindow)));
            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.LoadingWindow, new UIWindowContext("Exported/UI/Loading/Prefabs/LoadingWindow", typeof(LoadingWindow)));
        }
    }
}
