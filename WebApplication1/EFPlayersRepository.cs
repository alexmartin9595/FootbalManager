using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Configuration;
using manager.Data;
using manager.Entities;


namespace WebApplication1
{
    public class EFPlayersRepository
    {
        private EFDbContext currentContext;

        public EFPlayersRepository ()
        {
            currentContext = new EFDbContext(ConfigurationManager.ConnectionStrings[0].ConnectionString);            
        }

        public IEnumerable<Player> GetPlayers ()
        {
            return currentContext.Players;
        }


        public void AddPlayer ()
        {
            Player player = new Player {Age = 30, Atack = 82, Defence = 50, Id =8 , Name = "Андреас Иньеста", Position = "Полузащитник", Price=42000};
            currentContext.Players.Add(player);
            currentContext.SaveChanges();
            
        }
    }
}