using System;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class BattleInitData
    {
        public int id;
        public Action<int, string> actionOnDebug;
        public Func<int, EntityView> funcOnGetView;
        public List<TeamInitData> teamList = new List<TeamInitData>();
    }
}
