using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace manager.Entities
{
    public class ManagerUser
    {
        [Key]
        public int Id {get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> InputMessages { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> OutputMessages { get; set; }
        [JsonIgnore]
        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }
        [JsonIgnore]
        public virtual TeamData TeamData { get; set; }
        [JsonIgnore]
        public virtual ICollection<Match> FirstUserMatches { get; set; }
        [JsonIgnore]
        public virtual ICollection<Match> SecondUserMatches { get; set; }
        [JsonIgnore]
        public virtual ICollection<Goal> UserGoals { get; set; }

    }
}
