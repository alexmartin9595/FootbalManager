using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;
using manager.Data;

namespace manager.Business
{
    public class TeamPlayerLogic
    {
        private TeamPlayerStorage teamPlayerStorage;

        public TeamPlayerLogic()
        {
            teamPlayerStorage = new TeamPlayerStorage();
        }

        public IEnumerable<TeamPlayer> GetAllPlayers(int teamId)
        {
            return teamPlayerStorage.GetAllPlayers(teamId);
        }

        public TeamPlayer GeTeamPlayer(int playerId)
        {
            return teamPlayerStorage.GetPlayerById(playerId);
        }

        public void AddTeamPlayer(int teamId, TeamPlayer teamPlayer)
        {
            TeamPlayer currentTeamPlayer = teamPlayerStorage.GetPlayerByName(teamPlayer.Name, teamId);
            if (currentTeamPlayer != null)
                throw  new Exception("Такой футболист уже есть");
            teamPlayerStorage.AddPlayer(teamPlayer);
        }

        public void UpdateAttack(int playerId)
        {
            TeamPlayer teamPlayer = teamPlayerStorage.GetPlayerById(playerId);
            TeamDataLogic dataLogic = new TeamDataLogic();
            int currentBudget = dataLogic.GetTeamData(teamPlayer.TeamId).Budget;
            if (teamPlayer == null)
                throw new Exception("Такого футболиста нет");
            if (currentBudget < 1000)
                throw new Exception("Недостаточно средств");
            teamPlayer.Atack++;
            dataLogic.UpdateBudget(currentBudget - 1000, teamPlayer.TeamId);
            teamPlayerStorage.UpdatePlayer(playerId, teamPlayer);
        }

        public void UpdateDefence(int playerId)
        {
            TeamPlayer teamPlayer = teamPlayerStorage.GetPlayerById(playerId);
            TeamDataLogic dataLogic = new TeamDataLogic();
            int currentBudget = dataLogic.GetTeamData(teamPlayer.TeamId).Budget;
            if (teamPlayer == null)
                throw new Exception("Такого футболиста нет");
            if (currentBudget < 1000)
                throw new Exception("Недостаточно средств");
            teamPlayer.Defence++;
            dataLogic.UpdateBudget(currentBudget - 1000, teamPlayer.TeamId);
            teamPlayerStorage.UpdatePlayer(playerId, teamPlayer);
        }

        public void UpdatePrice(int playerId, int newPrice)
        {
            TeamPlayer teamPlayer = teamPlayerStorage.GetPlayerById(playerId);
            if (teamPlayer == null)
                throw new Exception("Такого футболиста нет");
            teamPlayer.Price = newPrice;
            teamPlayerStorage.UpdatePlayer(playerId, teamPlayer);
        }

        public void UpdateNumber(int playerId, int number)
        {
            TeamPlayer teamPlayer = teamPlayerStorage.GetPlayerById(playerId);
            if (teamPlayer == null)
                throw new Exception("Такого футболиста нет");
            try
            {
                if (number > 0 && number < 100)
                {
                    teamPlayer.Number = number;
                    teamPlayerStorage.UpdatePlayer(playerId, teamPlayer);
                }
                else
                {
                    throw new Exception("Некорректные данные");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Некорректные данные");
            }
        }

        public void DeletePlayer(int playerId)
        {
            TeamPlayer teamPlayer = teamPlayerStorage.GetPlayerById(playerId);
            if (teamPlayer == null)
                throw new Exception("Такого футболиста нет");
            teamPlayerStorage.DeletePlayer(teamPlayer);
        }
    }
}
