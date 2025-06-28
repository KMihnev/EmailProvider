using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.DBModels
{
    public enum Languages
    {
        LanguagesEnglish = 1
    }

    public partial class Language
    {
        public Language()
        {
            Countries = new HashSet<Country>();
            Users = new HashSet<User>();
            LangSupports = new HashSet<LangSupport>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public virtual ICollection<Country> Countries { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<LangSupport> LangSupports { get; set; }
    }
}
