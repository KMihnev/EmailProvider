using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceIntermediate.Models.Serializables
{
    public class UserSerializable
    {
        public UserSerializable()
        {
            CountryId = 1;
            PhoneNumber = string.Empty;
            Name = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
    }
}
