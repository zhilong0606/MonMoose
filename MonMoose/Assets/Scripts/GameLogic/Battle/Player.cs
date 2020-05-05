using System.Collections.Generic;

public class Player
{
    public int playerID;

    public ECampType camp;

    public string playerName;

    public bool isAI = false;

    public int actorID;

    public Actor selectedActor;

    public Actor captainActor;

    public List<Actor> actorList = new List<Actor>();
}
