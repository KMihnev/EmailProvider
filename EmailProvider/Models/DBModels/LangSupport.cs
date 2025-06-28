using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.DBModels
{
    public class LangSupport : IEntity
    {
        public int Id { get; set; }

        public string MessageName { get; set; } = null!;

        public string Value { get; set; } = null!;

        public int Context { get; set; }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; } = null!;
    }
}
