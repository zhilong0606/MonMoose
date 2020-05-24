using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class BattleInitData
    {
        public int id;
        public List<BattleTeamInitData> teamList = new List<BattleTeamInitData>();
    }
}
