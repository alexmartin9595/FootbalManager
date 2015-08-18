using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;

namespace manager.Data
{
    public class GoalStorage
    {
        private EFDbContext currentContext;

        public GoalStorage()
        {
            currentContext = new EFDbContext();
            currentContext.Configuration.LazyLoadingEnabled = true;
            currentContext.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<Goal> GetPlayersGoals(int playerId)
        {
            return currentContext.Goals.Where(x => x.PlayerId == playerId).Include(x => x.CurrentPlayer).Include(x => x.CurrentMatch);
        }

        public IEnumerable<Goal> GetMacthGoals(int matchId)
        {
            return currentContext.Goals.Where(x => x.MatchId == matchId).Include(x => x.CurrentPlayer).Include(x => x.CurrentMatch).Include(x => x.CurrentUser);
        }

        public void AddGoal(Goal goal)
        {
            currentContext.Goals.Add(goal);
            currentContext.SaveChanges();
        }

    }
}
