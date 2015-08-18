using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manager.Entities
{
    [Table("TeamDatas")]
    public class TeamData
    {
        [Key, ForeignKey("CurrentUser")]
        public int Id { get; set; }
        public int Budget { get; set; }
        public int PlayersNumber { get; set; }
        public int Rate { get; set; }
        public int MatchesPlayed { get; set; }
        public int MatchesWin { get; set; }
        public int MatchesDraw { get; set; }
        public int MatchesLose { get; set; }
        public int DefendersNumber { get; set; }
        public int MidfieldersNumber { get; set; }
        public int ForwardsNumber { get; set; }
        public virtual ManagerUser CurrentUser { get; set; }
    }
}
