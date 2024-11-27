using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.Serializables
{
    public class OutgoingMessageSerializable
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int? FileId { get; set; }
        public string Status { get; set; }
        public bool IsDraft { get; set; }
        public DateTime DateOfSend { get; set; }
    }
}
