using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class Controller
    {
        public List<Player> playerList = new List<Player>();

        public void AddPlayer(Player player)
        {
            playerList.Add(player);
            player.controller = this;
        }
    }
}
