using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.Serializables
{
    public class MessasgeSerializable
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public virtual ICollection<FileSerializable> Files { get; set; } = new List<FileSerializable>();

        public virtual UserSerializable Receiver { get; set; } = null!;

        public virtual UserSerializable Sender { get; set; } = null!;

        public virtual StatusSerializable Status { get; set; } = null!;
    }
}
