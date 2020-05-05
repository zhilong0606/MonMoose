using System.Collections.Generic;
using MonMoose.Core;

public class ActorManager : Singleton<ActorManager>
{
    public List<Actor> actorList = new List<Actor>();
    public List<Actor> heroList = new List<Actor>();

    public void UpdateLogic()
    {
        for (int i = 0; i < actorList.Count; i++)
        {
            actorList[i].UpdateLogic();
        }
    }

    public void AddHero(Actor actor)
    {
        actorList.Add(actor);
        heroList.Add(actor);
    }
}
