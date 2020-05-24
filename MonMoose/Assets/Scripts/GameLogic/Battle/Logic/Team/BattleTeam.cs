using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class BattleTeam
    {
        public int id;
        public ECampType camp;
        public string name;
        public bool isAI = false;
        public List<Actor> actorList = new List<Actor>();

        public void Init(BattleTeamInitData initData)
        {
            id = initData.id;
            camp = initData.camp;
            name = initData.name;
            isAI = initData.isAI;
            initData.actorList.Sort(ActorInitData.Sort);
            for (int i = 0; i < initData.actorList.Count; ++i)
            {
                Actor actor = new Actor();
            }
        }

        public void Tick()
        {
            for (int i = 0; i < actorList.Count; ++i)
            {
                actorList[i].Tick();
            }
        }

        public static int Sort(BattleTeam x, BattleTeam y)
        {
            return x.id.CompareTo(y.id);
        }
    }
}
