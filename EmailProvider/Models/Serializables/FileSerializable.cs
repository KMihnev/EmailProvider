using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.Serializables
{
    public class FileSerializable
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
    }
}
