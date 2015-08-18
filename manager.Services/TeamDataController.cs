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
    public class TeamDataController : ApiController
    {
        [Authorize]
        [ActionName("GetAllData")]
        public IEnumerable<TeamData> GetAllData()
        {
            TeamDataLogic logic = new TeamDataLogic();
            return logic.GetAllTeamDatas();
        }

        [Authorize]
        [ActionName("GetData")]
        public TeamData Get()
        {
            TeamDataLogic logic = new TeamDataLogic();
            UserLogic userLogic = new UserLogic();
            int id = userLogic.GetIdByNameUser(User.Identity.Name);
            return logic.GetTeamData(id);
        }

        [Authorize]
        [HttpPut]
        [ActionName("UpdateMatchesWin")]
        public void UpdateMatchesWin(int id)
        {
            TeamDataLogic teamDataLogic = new TeamDataLogic();
            MatchLogic matchLogic = new MatchLogic();
            teamDataLogic.UpdateMatchesWin(matchLogic.GetMatchById(id).FirstTeamId);
            teamDataLogic.UpdateMatchesLose(matchLogic.GetMatchById(id).SecondTeamId);
        }

        [Authorize]
        [HttpPut]
        [ActionName("UpdateMatchesDraw")]
        public void UpdateMatchesDraw(int id)
        {
            TeamDataLogic teamDataLogic = new TeamDataLogic();
            MatchLogic matchLogic = new MatchLogic();
            teamDataLogic.UpdateMatchesDraw(matchLogic.GetMatchById(id).FirstTeamId);
            teamDataLogic.UpdateMatchesDraw(matchLogic.GetMatchById(id).SecondTeamId);
        }

        [Authorize]
        [HttpPut]
        [ActionName("UpdateMatchesLose")]
        public void UpdateMatchesLose(int id)
        {
            TeamDataLogic teamDataLogic = new TeamDataLogic();
            MatchLogic matchLogic = new MatchLogic();
            teamDataLogic.UpdateMatchesLose(matchLogic.GetMatchById(id).FirstTeamId);
            teamDataLogic.UpdateMatchesWin(matchLogic.GetMatchById(id).SecondTeamId);
        }
    }
}
