using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using MonMoose.Core;
using MonMoose.StaticData;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleEventListener : IBattleEventListener
    {
        public void StagePrepare()
        {
            BattleShortCut.ChangeBattleSubState(EBattleState.Prepare);
        }

        public void FormationEmbattle(int actorId, int x, int y)
        {
            BattleGridView view = BattleShortCut.GetGridView(new GridPosition(x, y));
            //BattleShortCut.curBattleStage.FormationEmbattle(actorId)
            ActorStaticInfo entityInfo = StaticDataManager.instance.GetActor(actorId);
            GameObject go = GameObjectPoolManager.instance.Fetch(entityInfo.PrefabPath);
            go.transform.position = view.transform.position;
            BattlePrepareActorManager.instance.AddActor(actorId, go, view.gridPosition);
            EventManager.instance.Broadcast((int)EventID.BattlePrepare_ActorItemHide, actorId);
        }

        public void FormationExchange(int posX1, int posY1, int posX2, int posY2)
        {
            throw new System.NotImplementedException();
        }

        public void FormationRetreat(int posX, int posY)
        {
            throw new System.NotImplementedException();
        }

        public void FormationEnd()
        {
            EventManager.instance.Broadcast((int)EventID.BattlePrepare_Finish);
        }

        public void BattleStart()
        {
            BattleShortCut.ChangeBattleSubState(EBattleState.Main);
        }
    }
}
