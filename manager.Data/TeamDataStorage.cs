using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;

namespace manager.Data
{
    public class TeamDataStorage
    {
        private EFDbContext currentContext;
        public TeamDataStorage()
        {
            currentContext = new EFDbContext();
            currentContext.Configuration.LazyLoadingEnabled = true;
            currentContext.Configuration.ProxyCreationEnabled = false;
        }
        public IEnumerable<TeamData> GetAllTeamDatas()
        {
            return currentContext.TeamDatas.OrderByDescending(t => t.Rate).Include("CurrentUser").ToList();
        }

        public TeamData GetTeamData(int teamId)
        {
            return currentContext.TeamDatas.FirstOrDefault(x => x.Id == teamId);
        }

        public void AddTeamData(TeamData teamData)
        {
            currentContext.TeamDatas.Add(teamData);
            currentContext.SaveChanges();
        }

        public void UpdateTeamData(int teamId, TeamData teamData)
        {
            TeamData currentTeamData = GetTeamData(teamId);
            currentTeamData = teamData;
            currentContext.SaveChanges();
        }

        public void DeleteTeamData(TeamData teamData)
        {
            currentContext.TeamDatas.Remove(teamData);
            currentContext.SaveChanges();
        }
    }
}
