using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceIntermediate.Models.Serializables
{
    public class MessageSerializable
    {
        
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }

        public int Direction { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public virtual ICollection<FileSerializable> Files { get; set; } = new List<FileSerializable>();

    }
}
