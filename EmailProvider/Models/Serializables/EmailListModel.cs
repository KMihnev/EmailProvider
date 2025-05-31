using EmailProvider.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.Serializables
{
    public class EmailListModel
    {
        public EmailListModel()
        {

        }

        public int Id { get; set; }
        public string FromEmail { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;

        public DateTime DateOfRegistration { get; set; }

        public EmailDirections Direction { get; set; }

        public List<MessageRecipientSerializable> Recipients { get; set; } = new();

        public int FolderId { get; set; }
    }
}
