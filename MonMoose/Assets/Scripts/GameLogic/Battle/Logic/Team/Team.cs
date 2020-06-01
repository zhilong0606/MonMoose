using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class Team
    {
        public int id;
        public ECampType camp;
        public string name;
        public bool isAI = false;
        public List<Actor> actorList = new List<Actor>();

        public void Init(TeamInitData initData)
        {
            teamContext.team = this;

            id = initData.id;
            camp = initData.camp;
            name = initData.name;
            isAI = initData.isAI;
            initData.actorList.Sort(ActorInitData.Sort);
            for (int i = 0; i < initData.actorList.Count; ++i)
            {
                Actor actor = new Actor();

                //EntityContext context = 
            }
        }

        public void Tick()
        {
        }

        public static int Sort(Team x, Team y)
        {
            return x.id.CompareTo(y.id);
        }
    }
}
