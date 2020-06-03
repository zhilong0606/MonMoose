using System.Collections;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public enum EWindowId
    {
        None,
        GameInit,
        Lobby,
        Loading,
        BattleMain,
    }

    public static class UIWindowDefine
    {
        public static void Define()
        {
            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.GameInit, new UIWindowContext("UI/GameInit/Prefabs/GameInitWindow", typeof(GameInitWindow)));

            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.Lobby, new UIWindowContext("Exported/UI/Lobby/Prefabs/LobbyWindow", typeof(LobbyWindow)));
            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.Loading, new UIWindowContext("Exported/UI/Loading/Prefabs/LoadingWindow", typeof(LoadingWindow)));
            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.BattleMain, new UIWindowContext("Exported/UI/Battle/Prefabs/BattleMainWindow", typeof(BattleMainWindow)));
        }
    }
}
