using EmailServiceIntermediate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceIntermediate.Validation
{
    public class BaseValidator : IValidator
    {
        protected Dictionary<UserValidationTypes, string> ValidationFields { get; set; }

        public virtual bool Validate()
        {
            return true;
        }
    }
}
