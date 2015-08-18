using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using manager.Business;
using manager.Data;
using manager.Entities;
using manager.UserInterface.Models;
using Microsoft.AspNet.Identity;


namespace manager.UserInterface.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
       

        public ActionResult Market()
        {
            return View();
        }

        [Authorize]
        public ActionResult Table()
        {
            return View();
        }

         [Authorize]
        public ActionResult Stats()
        {
            return View();
        }

        [Authorize]
        public ActionResult Messages()
        {
            return View();
        }

        [Authorize]
        public ActionResult Match()
        {
            return View();
        }
    }
}
