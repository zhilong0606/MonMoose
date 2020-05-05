using System;
using System.Collections.Generic;
using MonMoose.Core;

public class PlayerManager : Singleton<PlayerManager>
{
    private List<Player> playerList = new List<Player>(10);
    private Player hostPlayer;

    public Player HostPlayer { get { return hostPlayer; } }

    public void AddPlayer(Player player, bool isHostPlayer)
    {
        playerList.Add(player);
        if (isHostPlayer)
        {
            hostPlayer = player;
        }
    }

    public Player GetPlayer(int playerID)
    {
        for (int i = 0; i < playerList.Count; ++i)
        {
            if (playerList[i].playerID == playerID)
            {
                return playerList[i];
            }
        }
        return null;
    }

    public void Ergodic(Action<Player> OnItemErgodic)
    {
        for (int i = 0; i < playerList.Count; ++i)
        {
            OnItemErgodic(playerList[i]);
        }
    }
}
