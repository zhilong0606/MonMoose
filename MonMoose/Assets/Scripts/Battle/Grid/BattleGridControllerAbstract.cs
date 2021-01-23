using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public abstract class BattleGridControllerAbstract : BattleViewController<BattleGrid>
    {
        public abstract void SetCanEmbattle(bool flag);
        public abstract void SetColor(float r, float g, float b);
    }
}
