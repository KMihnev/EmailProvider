using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.Serializables
{
    public class BulkOutgoingMessageSerializable
    {
        public int Id { get; set; }
        public string RawData { get; set; }
        public int? ScheduledDate { get; set; }
        public int OutgoingMessageId { get; set; }
    }
}
