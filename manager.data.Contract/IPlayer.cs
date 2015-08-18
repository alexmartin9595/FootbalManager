using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;

namespace manager.data.Contract
{
    public interface IPlayer
    {
        IEnumerable<Player> GetAllPlayers();
        IEnumerable<Player> GetPlayersByPosition(string position);
        IEnumerable<Player> GetPlayersByCostinterval(int minCost, int maxCost);
        IEnumerable<Player> GetPlayersByAgeinterval(int minAge, int maxAge);
        Player GetPlayerByName(string name);
        void AddPlayer(Player player);
        void UpdateOrInsertPlayer(Player player);
        void DeletePlayer(Player player);
    }
}
