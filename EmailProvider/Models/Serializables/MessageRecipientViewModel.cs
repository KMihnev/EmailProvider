using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.Serializables
{
    public class MessageRecipientSerializable
    {
        public MessageRecipientSerializable()
        {
            
        }

        public string Email { get; set; } = string.Empty!;
    }
}
