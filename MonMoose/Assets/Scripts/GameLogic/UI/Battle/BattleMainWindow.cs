using MonMoose.Core;
using UnityEngine;

namespace MonMoose.GameLogic.UI
{
    public class BattleMainWindow : UIWindow
    {
        protected override void OnInit(object param)
        {
            base.OnInit(param);

        }

        private KeyCode[] codes = new KeyCode[] {KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.X};

        protected override void Update()
        {
            base.Update();
            for (int i = 0; i < codes.Length; ++i)
            {
                if (Input.GetKeyDown(codes[i]))
                {
                    BattleState battleState = GameManager.instance.stateMachine.GetState((int)EGameState.Battle) as BattleState;
                    battleState.Send(codes[i]);
                }
            }
        }
    }
}
