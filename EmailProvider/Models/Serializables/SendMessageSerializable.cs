using EmailProvider.Models.Serializables.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceIntermediate.Models.Serializables
{
    public class SendMessageSerializable : SendMessageDTOBase
    {

        public SendMessageSerializable()
        {
        }

        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }

        public string SenderEmail { get; set; }

        public ICollection<string> ReceiverEmails { get; set; } = new List<string>();
        public DateTime DateOfCompletion { get; set; }
        public virtual ICollection<FileSerializable> Files { get; set; } = new List<FileSerializable>();

    }
}
