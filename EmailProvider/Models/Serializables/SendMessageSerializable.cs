using EmailProvider.Models.Serializables.Base;
using EmailServiceIntermediate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceIntermediate.Models.Serializables
{
    public class SendMessageSerializable : SendMessageDTOBase
    {

        public SendMessageSerializable() : base()
        {
            DateOfCompletion = DateTime.Now;
            Files = new List<FileSerializable>();
            Status = EmailStatusProvider.GetCompleteStatus();
        }

        public string Subject { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }

        public string SenderEmail { get; set; }

        public DateTime DateOfCompletion { get; set; }
        public virtual ICollection<FileSerializable> Files { get; set; }

    }
}
