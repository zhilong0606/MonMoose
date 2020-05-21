using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public class ActorManager : Singleton<ActorManager>
    {
        public List<Actor> actorList = new List<Actor>();
        public List<Actor> heroList = new List<Actor>();

        public void Tick()
        {
            for (int i = 0; i < actorList.Count; i++)
            {
                actorList[i].Tick();
            }
        }

        public void AddHero(Actor actor)
        {
            actorList.Add(actor);
            heroList.Add(actor);
        }
    }
}
