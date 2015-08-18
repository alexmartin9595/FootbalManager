using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using manager.Entities;

namespace manager.UserInterface.Models
{
    public class MainPageViewModel
    {
        public ManagerUser User { get; set; }
        public IEnumerable<TeamPlayer> TeamPlayer { get; set; }
        public TeamData teamData { get; set; }
        public IEnumerable<Player> Players { get; set; }

    }
}