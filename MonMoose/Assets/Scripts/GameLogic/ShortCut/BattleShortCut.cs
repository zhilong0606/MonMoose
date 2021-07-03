using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using MonMoose.GameLogic.Battle;
using UnityEngine;

namespace MonMoose.GameLogic
{
    public static class BattleShortCut
    {
        public static GameStateBattle battleState
        {
            get { return GameManager.instance.stateMachine.GetState((int)EGameState.Battle) as GameStateBattle; }
        }

        public static BattleStage curBattleStage
        {
            get { GameStateBattle state = battleState; if (state != null) return state.curStage; return null; }
        }

        public static BattleScene battleScene
        {
            get { GameStateBattle state = battleState; if (state != null) return state.battleScene; return null; }
        }

        public static BattleBase battleInstance
        {
            get { GameStateBattle state = battleState; if (state != null) return state.battleInstance; return null; }
        }

        public static FrameSyncSender frameSyncSender
        {
            get { BattleBase battle = battleInstance; if (battle != null) return battle.sender; return null; }
        }

        public static GameObject actorRoot
        {
            get { BattleScene scene = battleScene; if (scene != null) return scene.actorRoot; return null; }
        }

        public static void AddGridView(BattleGridView view)
        {
            BattleScene scene = battleScene; if (scene != null) scene.AddGridView(view);
        }

        public static BattleGridView GetGridView(GridPosition gridPos)
        {
            BattleScene scene = battleScene; if (scene != null) return scene.GetGridView(gridPos); return null;
        }

        public static BattleGridView GetGridViewByScreenPosition(Vector2 screenPos)
        {
            BattleScene scene = battleScene; if (scene != null) return scene.GetGridViewByScreenPosition(screenPos); return null;
        }

        public static Vector3 GetGridWorldPosition(GridPosition gridPos, DcmVec2 offset)
        {
            BattleScene scene = battleScene; if (scene != null) return scene.GetGridWorldPosition(gridPos, offset); return Vector3.zero;
        }

        public static void ChangeBattleState(EBattleState state)
        {
            if (battleState != null) battleState.stateMachine.ChangeState((int)state);
        }
    }
}
