using EmailProvider.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.Serializables
{
    public class FolderViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public EmailDirections FolderDirection {  get; set; }
    }
}
