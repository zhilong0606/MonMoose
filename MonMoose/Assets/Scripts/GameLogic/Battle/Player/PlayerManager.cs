using System;
using System.Collections.Generic;
using MonMoose.Core;

public class PlayerManager : Singleton<PlayerManager>
{
    private List<Player> playerList = new List<Player>();

    public void AddPlayer(Player player)
    {
        playerList.Add(player);
    }

    public Player GetPlayer(int playerId)
    {
        for (int i = 0; i < playerList.Count; ++i)
        {
            if (playerList[i].playerId == playerId)
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
