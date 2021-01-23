using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public interface IBattleEventListener
    {
        void StagePrepare();
        void FormationEmbattle(int actorId, int x, int y);
        void FormationEnd();
        void BattleStart();
        void FormationExchange(int posX1, int posY1, int posX2, int posY2);
        void FormationRetreat(int posX, int posY);
    }
}
