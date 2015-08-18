using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using manager.Business;
using manager.Entities;

namespace manager.Services
{
    public class MatchController : ApiController
    {
        [Authorize]
        [HttpGet]
        [ActionName("CalculateFirstTeamScore")]
        public int CalculateFirstTeamScore(int id)
        {
            object locker = new object();
            MatchLogic matchLogic = new MatchLogic();
            int firstTeamId = matchLogic.GetMatchById(id).FirstTeamId;
            int secondTeamId = matchLogic.GetMatchById(id).SecondTeamId;
            int minute = matchLogic.GetCurrentMinute(id);
            lock (locker)
            {
                matchLogic.CalculateMinute(firstTeamId, secondTeamId, id, minute);
                matchLogic.UpdateTimer(id);
            }
            return matchLogic.GetMatchById(id).FirstTeamGoals;
        }

        [Authorize]
        [HttpGet]
        [ActionName("CalculateSecondTeamScore")]
        public int CalculateSecondTeamScore(int id)
        {
            MatchLogic matchLogic = new MatchLogic();
            return matchLogic.GetMatchById(id).SecondTeamGoals;
        }

        [Authorize]
        [HttpGet]
        [ActionName("GetFirstTeamScore")]
        public int GetFirstTeamScore(int id)
        {
            MatchLogic matchLogic = new MatchLogic();
            return matchLogic.GetMatchById(id).FirstTeamGoals;
        }

        [Authorize]
        [HttpGet]
        [ActionName("GetSecondTeamScore")]
        public int GetFirstSecondScore(int id)
        {
            MatchLogic matchLogic = new MatchLogic();
            return matchLogic.GetMatchById(id).SecondTeamGoals;
        }

        [Authorize]
        [ActionName("GetGoalscorers")]
        public IEnumerable<Goal> GetGoalscorers(int id)
        {
            GoalLogic goalLogic = new GoalLogic();
            return goalLogic.GetMacthGoals(id);
        }

        [Authorize]
        [ActionName("GetCurrentMinute")]
        public int GetCurrentMinute(int id)
        {
            MatchLogic matchLogic = new MatchLogic();
            return matchLogic.GetCurrentMinute(id);
        }

        [Authorize]
        [HttpPost]
        [ActionName("AddMatch")]
        public int AddMacth([FromBody] Match match)
        {
            try
            {
                MatchLogic matchLogic = new MatchLogic();
                matchLogic.AddMatch(match);
            }
            catch (Exception)
            {
                return match.FirstTeamId;
            }
            return match.Id;
        }

        [Authorize]
        [HttpGet]
        [ActionName("GetFirstTeamId")]
        public int GetFirstTeamId(int id)
        {
            MatchLogic matchLogic = new MatchLogic();
            return matchLogic.GetMatchById(id).FirstTeamId;
        }

        [Authorize]
        [HttpGet]
        [ActionName("GetSecondTeamId")]
        public int GetSecondTeamId(int id)
        {
            MatchLogic matchLogic = new MatchLogic();
            return matchLogic.GetMatchById(id).SecondTeamId;
        }

        [Authorize]
        [HttpGet]
        [ActionName("GetTeamMatches")]
        public IEnumerable<Match> GetTeamMatches()
        {
            MatchLogic matchLogic = new MatchLogic();
            UserLogic userLogic = new UserLogic();
            int id = userLogic.GetIdByNameUser(User.Identity.Name);
            return matchLogic.GetMatchesByUser(id);
        }

    }
}
