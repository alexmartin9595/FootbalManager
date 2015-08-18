using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Data;
using  manager.Entities;

namespace manager.Business
{
    public class TeamDataLogic
    {
        private TeamDataStorage teamDataStorage;

        public TeamDataLogic()
        {
            teamDataStorage = new TeamDataStorage();
        }

        public IEnumerable<TeamData> GetAllTeamDatas()
        {
            return teamDataStorage.GetAllTeamDatas();
        }

        public TeamData GetTeamData(int teamId)
        {
            return teamDataStorage.GetTeamData(teamId);
        }

        public void AddTeamData(TeamData teamData)
        {
            TeamData currentTeamData = teamDataStorage.GetTeamData(teamData.Id);
            if (currentTeamData != null)
                throw new Exception("Такая команда уже есть");
            teamDataStorage.AddTeamData(teamData);
        }

        public void UpdateBudget(int budget, int teamId)
        {
            TeamData teamData = teamDataStorage.GetTeamData(teamId);
            if (teamData == null)
                throw new Exception("Такой команды нет");
            teamData.Budget = budget;
            teamDataStorage.UpdateTeamData(teamId, teamData);
        }

        public void UpdateRate(int rate, int teamId)
        {
            TeamData teamData = teamDataStorage.GetTeamData(teamId);
            if (teamData == null)
                throw new Exception("Такой команды нет");
            teamData.Rate = rate;
            teamDataStorage.UpdateTeamData(teamId, teamData);
        }

        public void UpdatePlayerNumber(int teamId)
        {
            TeamData teamData = teamDataStorage.GetTeamData(teamId);
            if (teamData == null)
                throw new Exception("Такой команды нет");
            teamData.PlayersNumber++;
            teamDataStorage.UpdateTeamData(teamId, teamData);
        }

        public void UpdateMatchesNumber(int teamId)
        {
            TeamData teamData = teamDataStorage.GetTeamData(teamId);
            if (teamData == null)
                throw new Exception("Такой команды нет");
            teamData.MatchesPlayed++;
            teamDataStorage.UpdateTeamData(teamId, teamData);
        }

        public void UpdateMatchesWin(int teamId)
        {
             UpdateMatchesNumber(teamId);
             TeamData teamData = teamDataStorage.GetTeamData(teamId);
             teamData.MatchesWin++;
             teamData.Rate += 3;
             teamData.Budget += 3000;
             teamDataStorage.UpdateTeamData(teamId, teamData);
        }

        public void UpdateMatchesDraw(int teamId)
        {
            UpdateMatchesNumber(teamId);
            TeamData teamData = teamDataStorage.GetTeamData(teamId);
            teamData.MatchesDraw++;
            teamData.Rate++;
            teamData.Budget += 1000;
            teamDataStorage.UpdateTeamData(teamId, teamData);
        }

        public void UpdateMatchesLose(int teamId)
        {
            UpdateMatchesNumber(teamId);
            TeamData teamData = teamDataStorage.GetTeamData(teamId);
            teamData.MatchesLose++;
            teamData.Budget -= 1000;
            teamDataStorage.UpdateTeamData(teamId, teamData);
        }

        public void DeleteTeamData(int teamId)
        {
            TeamData teamData = teamDataStorage.GetTeamData(teamId);
            if (teamData == null)
                throw new Exception("Такой команды нет");
            teamDataStorage.DeleteTeamData(teamData);
        }
    }
}
