using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;

namespace manager.Data
{
    public class MatchStorage
    {
        private EFDbContext currentContext;

        public MatchStorage()
        {
            currentContext = new EFDbContext();
            currentContext.Configuration.LazyLoadingEnabled = true;
            currentContext.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<Match> GetMatchesByUser(int teamId)
        {
            return currentContext.Matches.Where(m => m.FirstTeamId == teamId || m.SecondTeamId == teamId)
                 .Include(m => m.FirstUser).Include(m => m.SecondUser).ToList();
        }

        public Match GetMatchById(int id)
        {
            return currentContext.Matches.FirstOrDefault(m => m.Id == id);
        }

        public void AddMacth(Match match)
        {
            currentContext.Matches.Add(match);
            currentContext.SaveChanges();
        }

        public void UpdateMatch(int macthId, Match match)
        {
            Match currentMatch = GetMatchById(macthId);
            currentMatch.FirstTeamGoals = match.FirstTeamGoals;
            currentMatch.SecondTeamGoals = match.SecondTeamGoals;
            currentContext.SaveChanges();
        }
    }
}
