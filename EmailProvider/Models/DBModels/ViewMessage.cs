using EmailServiceIntermediate.Models.Serializables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.DBModels
{
    public class ViewMessage
    {
        public int MessageId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmails { get; set; }
        public List<string>? ReceiverEmailsList { get; set; } = new List<string>();
        public DateTime DateOfCompletion { get; set; }

        public int Status { get; set; }
    }
}
