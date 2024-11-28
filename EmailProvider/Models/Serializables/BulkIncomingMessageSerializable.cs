using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceIntermediate.Models.Serializables
{
    public class BulkIncomingMessageSerializable
    {
        public int Id { get; set; }
        public string RawData { get; set; }
    }
}
