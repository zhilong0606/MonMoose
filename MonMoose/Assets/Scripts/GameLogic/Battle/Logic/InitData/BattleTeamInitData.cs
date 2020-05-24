using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class BattleTeamInitData
    {
        public int id;
        public ECampType camp;
        public string name;
        public List<ActorInitData> actorList = new List<ActorInitData>();
        public bool isAI = false;

        public static int Sort(BattleTeamInitData x, BattleTeamInitData y)
        {
            return x.id.CompareTo(y.id);
        }
    }
}
