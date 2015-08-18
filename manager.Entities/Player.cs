using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manager.Entities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public int Atack { get; set; }
        public int Defence { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Price { get; set; }
        public string Position { get; set; }
    }

    
}
