using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using manager.Entities;
using manager.Data;
using UserInterface.Models;
using manager.Entities;

namespace UserInterface.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TeamPlayerStorage teamPlayerStorage = new TeamPlayerStorage();
            var model = new MainPageViewModel();
            model.TeamPlayer = teamPlayerStorage.GetAllPlayers(1);
            TeamDataStorage teamDataStorage = new TeamDataStorage();
            model.teamData = teamDataStorage.GetTeamData(1);
            return View(model);
        }
       

        public ActionResult Market()
        {
            PlayerStorage playerStorage = new PlayerStorage();
            var model = new MainPageViewModel();
            model.Players = playerStorage.GetAllPlayers();
            return View(model);
        }

        public ActionResult Table()
        {
            UserStorage userStorage = new UserStorage();
            var model = new MainPageViewModel();
            model.Users= userStorage.GetAspNetUsers();
            return View(model);
        }
       
    }
}