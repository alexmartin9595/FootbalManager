using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Data;
using manager.Entities;

namespace manager.Business
{
    public class GoalLogic
    {
        private GoalStorage goalStorage;

        public GoalLogic()
        {
            goalStorage = new GoalStorage();
        }

        public IEnumerable<Goal> GetPlayersGoals(int playerId)
        {
            return goalStorage.GetPlayersGoals(playerId);
        }

        public IEnumerable<Goal> GetMacthGoals(int matchId)
        {
            return goalStorage.GetMacthGoals(matchId);
        }

        public void AddGoal(Goal goal)
        {
            goalStorage.AddGoal(goal);
        }
    }
}
