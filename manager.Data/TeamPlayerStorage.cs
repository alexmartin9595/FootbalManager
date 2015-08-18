using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;

namespace manager.Data
{
    public class TeamPlayerStorage
    {
        private EFDbContext currentContext;
        public TeamPlayerStorage()
        {
            currentContext = new EFDbContext();
            currentContext.Configuration.LazyLoadingEnabled = true;
            currentContext.Configuration.ProxyCreationEnabled = false;
        }
        public IEnumerable<TeamPlayer> GetAllPlayers(int teamId)
        {
            return currentContext.TeamPlayers.Where(x => x.TeamId == teamId).OrderBy(x => x.Number).Include(x => x.CurrentUser);
        }

        public TeamPlayer GetPlayerById(int playerId)
        {
            return currentContext.TeamPlayers.FirstOrDefault(x => x.Id == playerId);
        }

        public TeamPlayer GetPlayerByName(string name, int teamId)
        {
            return currentContext.TeamPlayers.FirstOrDefault(x => x.Name == name && x.TeamId == teamId);
        }

        public void AddPlayer(TeamPlayer teamPlayer)
        {
            currentContext.TeamPlayers.Add(teamPlayer);
            currentContext.SaveChanges();
        }

        public void UpdatePlayer(int playerId, TeamPlayer teamPlayer)
        {
            TeamPlayer currentTeamPlayer = GetPlayerById(playerId);
            currentTeamPlayer = teamPlayer;
            currentContext.SaveChanges();
        }

        public void DeletePlayer(TeamPlayer teamPlayer)
        {
            currentContext.TeamPlayers.Remove(teamPlayer);
            currentContext.SaveChanges();
        }
        
        
    }
}
