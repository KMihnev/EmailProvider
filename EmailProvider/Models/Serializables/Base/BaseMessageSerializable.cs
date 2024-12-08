using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.Serializables.Base
{
    public abstract class BaseMessageSerializable
    {
        protected BaseMessageSerializable()
        {
            ReceiverEmails = new List<string>();
        }

        public int Id { get; set; }
        public int SenderId { get; set; }
        public IEnumerable<string> ReceiverEmails { get; set; }
    }
}
