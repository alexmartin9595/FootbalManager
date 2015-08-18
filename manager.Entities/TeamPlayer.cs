using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace manager.Entities
{
    public class TeamPlayer
    {
        public int Id { get; set; }
        [ForeignKey("CurrentUser")]
        public int TeamId { get; set; }
        public int Atack { get; set; }
        public int Defence { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Price { get; set; }
        public string Position { get; set; }
        public int Number { get; set; }
        public virtual ManagerUser CurrentUser { get; set; }
        [JsonIgnore]
        public virtual ICollection<Goal> TeamGoals { get; set; }


    }
}
