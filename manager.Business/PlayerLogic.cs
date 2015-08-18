using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using  manager.Data;
using  manager.Entities;

namespace manager.Business
{
    public class PlayerLogic
    {
        private PlayerStorage playerStorage;

        public PlayerLogic()
        {
            playerStorage = new PlayerStorage();
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return playerStorage.GetAllPlayers();
        }

        public Player GetPlayerById(int playerId)
        {
            return playerStorage.GetPlayerById(playerId);
        }

        public IEnumerable<Player> GetPlayersByPosition(string position)
        {
            return playerStorage.GetPlayersByPosition(position);
        }

        public void BuyPlayer(int playerId, int teamId)
        {
            Player player = playerStorage.GetPlayerById(playerId);
            TeamDataLogic teamDataLogic = new TeamDataLogic();
            TeamData teamData = teamDataLogic.GetTeamData(teamId);
            int playersNumber = teamData.PlayersNumber;
            if (playersNumber == 11)
                throw new Exception("Команда полностью укомплектована");
            TeamPlayerLogic teamPlayerLogic = new TeamPlayerLogic();
            int budget = teamData.Budget;
            if (budget  < player.Price)
                throw new Exception("Недостаточно денежных средств");
            TeamPlayer teamPlayer = new TeamPlayer
            {
                Age = player.Age,
                Name = player.Name,
                Position = player.Position,
                Atack = player.Atack,
                Defence = player.Defence,
                Price = player.Price,
                TeamId = teamId,
                Number = playersNumber + 1
            };
            teamPlayerLogic.AddTeamPlayer(teamId, teamPlayer);
            teamDataLogic.UpdateBudget(budget - player.Price, teamId);
            teamDataLogic.UpdatePlayerNumber(teamId);
        }
          

        public void AddPlayer(Player player)
        {
            Player currentPlayer = playerStorage.GetPlayerByName(player.Name);
            if (currentPlayer != null)
                throw new Exception("Такой игрок уже есть");
            playerStorage.AddPlayer(player);
        }

        public void UpdateOrInsertPlayer(string playerName)
        {
            Player currentPlayer = playerStorage.GetPlayerByName(playerName);
            if (currentPlayer == null)
                throw new Exception("Такого футболиста нет");
            playerStorage.UpdateOrInsertPlayer(currentPlayer);
        }

        public void DeletePlayer(string playerName)
        {
            Player currentPlayer = playerStorage.GetPlayerByName(playerName);
            if (currentPlayer == null)
                throw new Exception("Такого футболиста нет");
            playerStorage.DeletePlayer(currentPlayer);           
        }
    }
}
