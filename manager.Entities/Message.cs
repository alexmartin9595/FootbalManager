using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace manager.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public bool IsInvitation { get; set; }
        public string Text { get; set; }
        public bool IsRefused { get; set; }
        public bool IsSeen { get; set; }
        [ForeignKey("SenderId")]
        public virtual ManagerUser SenderUser { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual ManagerUser ReceiverUser { get; set; }

    }
}
