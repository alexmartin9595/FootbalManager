using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using manager.Entities;
using manager.Data;
using WebApplication1.Models;
using manager.Entities;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PlayerStorage playerStorage = new PlayerStorage();
            var model = new MainPageViewModel();
            //model.Players = playerStorage.GetPlayersByAgeinterval(20, 25);
            TeamDataStorage teamDataStorage = new TeamDataStorage();
            Player player = new Player
            {
                Name = "Рафаэль Варан",
                Age = 21,
                Atack = 35,
                Defence = 80,
                Position = "защитник",
                Price = 20000
            };
            //playerStorage.AddPlayer(player);



            TeamData teamData = new TeamData
            {
                Budget = 100000,
                Rate = 10,
                PlayersNumber = 11,
                MatchesPlayed = 20,
                MatchesWin = 10,
                MatchesDraw = 7,
                MatchesLose = 3
            };
            teamDataStorage.AddTeamData(teamData);

            
            //model.teamData = teamDataStorage.GetTeamData(0);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Футбольный менеджер";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Stats()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}