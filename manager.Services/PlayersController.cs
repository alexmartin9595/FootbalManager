using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using manager.Business;
using manager.Entities;

namespace manager.Services
{
    public class PlayersController : ApiController
    {
        [ActionName("GetPlayers")]
        public IEnumerable<Player> Get()
        {
            PlayerLogic logic = new PlayerLogic();
            return logic.GetAllPlayers();
        }

        [ActionName("GetForwards")]
        public IEnumerable<Player> GetForwards()
        {
            PlayerLogic logic = new PlayerLogic();
            return logic.GetPlayersByPosition("Нападающий");
        }

        [ActionName("GetMidfielders")]
        public IEnumerable<Player> GetMidfielders()
        {
            PlayerLogic logic = new PlayerLogic();
            return logic.GetPlayersByPosition("Полузащитник");
        }

        [ActionName("GetDefenders")]
        public IEnumerable<Player> GetDefenders()
        {
            PlayerLogic logic = new PlayerLogic();
            return logic.GetPlayersByPosition("Защитник");
        }

        [ActionName("GetCeapers")]
        public IEnumerable<Player> GetCeapers()
        {
            PlayerLogic logic = new PlayerLogic();
            return logic.GetPlayersByPosition("Вратарь");
        }
        
       
        [HttpPost]
        [ActionName("BuyPlayer")]
        public string BuyPlayer(int Id)
        {
            if (!User.Identity.IsAuthenticated)
                return "Авторизируйтесь для покупки игроков";
            PlayerLogic logic = new PlayerLogic();
            UserLogic userLogic = new UserLogic();
            int teamId = userLogic.GetIdByNameUser(User.Identity.Name);
            try
            {
                logic.BuyPlayer(Id, teamId);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            return "Трансфер совершён успешно";
        }
    }
}
