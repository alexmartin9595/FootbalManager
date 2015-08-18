using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  manager.Entities;




namespace WebApplication1.Models
{
    public class MainPageViewModel
    {
        public TeamData teamData { get; set; }
        public IEnumerable<Player> Players { get; set; }

    }
}