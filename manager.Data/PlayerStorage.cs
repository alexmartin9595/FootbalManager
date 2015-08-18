using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;

namespace manager.Data
{
    public class PlayerStorage
    {
        private EFDbContext currentContext;

        public PlayerStorage()
        {
            currentContext = new EFDbContext();            
        }

        public IEnumerable<Player> GetAllPlayers ()
        {
            return currentContext.Players;
        }

        public IEnumerable<Player> GetPlayersByPosition(string position)
        {
            return currentContext.Players.Where(x => x.Position == position);
        }

        public Player GetPlayerById(int id)
        {
            return currentContext.Players.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Player> GetPlayersByCostinterval(int minCost, int maxCost)
        {
            return currentContext.Players.Where(x => x.Price < maxCost && x.Price > minCost);
        }

        public IEnumerable<Player> GetPlayersByAgeinterval(int minAge, int maxAge)
        {
            return currentContext.Players.Where(x => x.Age < maxAge && x.Age > minAge);
        }

        public Player GetPlayerByName(string name)
        {
            return currentContext.Players.First(x => x.Name == name);
        }


        public void AddPlayer(Player player)
        {
            currentContext.Players.Add(player);
            currentContext.SaveChanges();
        }

        public void UpdateOrInsertPlayer(Player player)
        {
            
        }

        public void DeletePlayer(Player player)
        {
            currentContext.Players.Remove(player);
            currentContext.SaveChanges();
        }
    }
}
