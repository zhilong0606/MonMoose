using System.Collections;
using MonMoose.Core;
using MonMoose.GameLogic.UI;
using MonMoose.StaticData;

namespace MonMoose.GameLogic
{
    public enum EWindowId
    {
        None,
        GameInit,
        Lobby,
        Loading,
        BattleMain,
        BattlePrepare,
        BattlePrepareCover,
    }

    public static class UIWindowDefine
    {
        public static void DefineBeforeGameInit()
        {
            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.GameInit, new UIWindowContext("UI/GameInit/Prefabs/GameInitWindow", typeof(GameInitWindow)));
        }

        public static void DefineAfterGameInit()
        {
            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.Lobby, new UIWindowContext(StaticDataShortCut.GetPrefabPath(EPrefabPathId.LobbyWindow), typeof(LobbyWindow)));
            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.Loading, new UIWindowContext(StaticDataShortCut.GetPrefabPath(EPrefabPathId.LoadingWindow), typeof(LoadingWindow)));
            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.BattleMain, new UIWindowContext(StaticDataShortCut.GetPrefabPath(EPrefabPathId.BattleMainWindow), typeof(BattleMainWindow)));
            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.BattlePrepare, new UIWindowContext(StaticDataShortCut.GetPrefabPath(EPrefabPathId.BattlePrepareWindow), typeof(BattlePrepareWindow)));
            UIWindowManager.instance.RegisterWindowContext((int)EWindowId.BattlePrepareCover, new UIWindowContext(StaticDataShortCut.GetPrefabPath(EPrefabPathId.BattlePrepareCoverWindow), typeof(BattlePrepareCoverWindow)));
        }
    }
}
