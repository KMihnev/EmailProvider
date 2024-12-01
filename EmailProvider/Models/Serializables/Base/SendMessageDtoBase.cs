using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.Serializables.Base
{
    public abstract class SendMessageDTOBase
    {
        public int SenderId { get; set; }
        public IEnumerable<string> ReceiverEmails { get; set; } = new List<string>();
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int Status { get; set; }
        public DateTime DateOfCompletion { get; set; } = DateTime.Now;
    }
}
