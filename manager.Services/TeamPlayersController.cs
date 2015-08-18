using System;
using System.Collections.Generic;
using System.Web.Http;
using manager.Business;
using manager.Entities;

namespace manager.Services 
{
    public class TeamPlayersController : ApiController
    {
        [Authorize]
        [ActionName("GetPlayers")]
        public IEnumerable<TeamPlayer> Get()
        {
            TeamPlayerLogic logic = new TeamPlayerLogic();
            UserLogic userLogic = new UserLogic();
            int id = userLogic.GetIdByNameUser(User.Identity.Name);
            return logic.GetAllPlayers(id);
        }

        [Authorize]
        [ActionName("GetTeamPlayers")]
        public IEnumerable<TeamPlayer> GetTeamPlayers(int id)
        {
            TeamPlayerLogic logic = new TeamPlayerLogic();
            return logic.GetAllPlayers(id);
        }

        [Authorize]
        [HttpPost]
        [ActionName("AddAttack")]
        public string AddAttack(int id)
        {
            TeamPlayerLogic logic = new TeamPlayerLogic();
            try
            {
                logic.UpdateAttack(id);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            return "Игрок успешно прокачан";
        }

        [Authorize]
        [HttpPost]
        [ActionName("AddDefence")]
        public string AddDefence(int id)
        {
            TeamPlayerLogic logic = new TeamPlayerLogic();
            try
            {
                logic.UpdateDefence(id);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            return "Игрок успешно прокачан";
        }

        [Authorize]
        [HttpPost]
        [ActionName("UpdateNumber")]
        public string UpdateNumber(int id, [FromBody] TeamPlayer player)
        {
            TeamPlayerLogic logic = new TeamPlayerLogic();
            try
            {
                logic.UpdateNumber(id, player.Number);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            return "Номер успешно обновлён";
        }
    }
}
