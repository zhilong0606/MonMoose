using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class TeamInitData
    {
        public int id;
        public ECampType camp;
        public string name;
        public List<EntityInitData> actorList = new List<EntityInitData>();
        public bool isAI = false;

        public static int Sort(TeamInitData x, TeamInitData y)
        {
            return x.id.CompareTo(y.id);
        }
    }
}
