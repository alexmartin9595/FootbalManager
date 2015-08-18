using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using manager.Entities;

namespace UserInterface.Models
{
    public class MainPageViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<TeamPlayer> TeamPlayer { get; set; }
        public TeamData teamData { get; set; }
        public IEnumerable<Player> Players { get; set; }

    }
}