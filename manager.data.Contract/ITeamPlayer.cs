using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;

namespace manager.data.Contract
{
    public interface ITeamPlayer
    {
        IEnumerable<TeamPlayer> GetAllPlayers(int teanId);
        TeamPlayer GetPlayerById(int playerId);
        void AddPlayer(TeamPlayer teamPlayer);
        void UpdatePlayer(int playerId, TeamPlayer teamPlayer);
        void DeletePlayer(TeamPlayer teamPlayer);
    }
}
