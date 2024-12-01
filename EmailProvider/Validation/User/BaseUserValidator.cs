using EmailProvider.Validation.Base;
using EmailServiceIntermediate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Validation.User
{
    public class BaseUserValidator : IValidator
    {
        protected Dictionary<UserValidationTypes, string> ValidationFields { get; set; }

        public virtual bool Validate(bool bLog = false)
        {
            return true;
        }
    }
}
