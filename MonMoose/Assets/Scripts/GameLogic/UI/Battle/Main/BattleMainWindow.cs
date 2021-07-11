using MonMoose.Core;
using UnityEngine;

namespace MonMoose.GameLogic.UI
{
    public class BattleMainWindow : UIWindow
    {
        protected override void OnInit(object param)
        {
            base.OnInit(param);
            GetInventory().AddComponent<BattleMainNotifyWidget>((int)EWidget.MainNotify, true);
            GetInventory().AddComponent<BattleMainSelecedActorInfoWidget>((int)EWidget.SelectedActorInfo, true);
        }

        private KeyCode[] codes = new KeyCode[] {KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.X};

        protected override void Update()
        {
            base.Update();
            for (int i = 0; i < codes.Length; ++i)
            {
                if (Input.GetKeyDown(codes[i]))
                {
                    GameStateBattle battleState = GameManager.instance.stateMachine.GetState((int)EGameState.Battle) as GameStateBattle;
                    battleState.Send(codes[i]);
                }
            }
        }

        private enum EWidget
        {
            MainNotify,
            SelectedActorInfo,
        }
    }
}
