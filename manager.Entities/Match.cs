using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace manager.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int FirstTeamId { get; set; }
        public int SecondTeamId { get; set; }
        public int CurrentMinute { get; set; }
        public bool IsStarted { get; set; }
        public int FirstTeamGoals { get; set; }
        public int SecondTeamGoals { get; set; }
        [ForeignKey("FirstTeamId")]
        public virtual ManagerUser FirstUser { get; set; }
        [ForeignKey("SecondTeamId")]
        public virtual ManagerUser SecondUser { get; set; }
        [JsonIgnore]
        public virtual ICollection<Goal> TeamGoals { get; set; }


    }
}
