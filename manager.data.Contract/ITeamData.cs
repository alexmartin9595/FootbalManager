using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;

namespace manager.data.Contract
{
    public interface ITeamData
    {
        IEnumerable<TeamData> GetAllTeamDatas();
        TeamData GetTeamData(int teamId);
        void AddTeamData(TeamData teamData);
        void UpdateTeamData(int teamId, TeamData teamData);
        void DeleteTeamData(TeamData teamData);
    }
}
