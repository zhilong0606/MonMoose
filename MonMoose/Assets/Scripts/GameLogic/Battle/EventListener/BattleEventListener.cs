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
            BattleShortCut.ChangeBattleState(EBattleState.Prepare);
        }

        public void FormationEmbattle(int actorId, int x, int y)
        {
            EventManager.instance.Broadcast((int)EventID.BattlePrepare_ActorItemHide, actorId);
        }

        public void FormationExchange(int posX1, int posY1, int posX2, int posY2)
        {
        }

        public void FormationRetreat(int posX, int posY)
        {
        }

        public void FormationEnd()
        {
            EventManager.instance.Broadcast((int)EventID.BattlePrepare_Finish);
        }

        public void MainBattleStart()
        {
            BattleShortCut.ChangeBattleState(EBattleState.Main);
        }

        public void MainBattleEnd()
        {
            //BattleShortCut.ChangeBattleState(EBattleState.Main);
        }
    }
}
