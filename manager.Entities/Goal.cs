using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manager.Entities
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public int Minute { get; set; }
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public virtual TeamPlayer CurrentPlayer { get; set; }
        [ForeignKey("MatchId")]
        public virtual Match CurrentMatch { get; set; }
        [ForeignKey("TeamId")]
        public virtual ManagerUser CurrentUser { get; set; }


    }
}
