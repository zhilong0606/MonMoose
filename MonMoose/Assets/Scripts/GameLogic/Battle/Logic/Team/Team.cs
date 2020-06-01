using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class Team : BattleObj
    {
        public int id;
        public ECampType camp;
        public string name;
        public bool isAI = false;
        public List<Entity> entityList = new List<Entity>();

        public void Init(TeamInitData initData)
        {
            id = initData.id;
            camp = initData.camp;
            name = initData.name;
            isAI = initData.isAI;
            initData.actorList.Sort(ActorInitData.Sort);
            for (int i = 0; i < initData.actorList.Count; ++i)
            {
                ActorInitData actorInitData = initData.actorList[i];
                Entity entity = BattleFactory.CreateEntity(m_battleInstance, actorInitData.id, m_battleInstance.CreateObjId(EBattleObjType.Entity));
                entityList.Add(entity);
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
