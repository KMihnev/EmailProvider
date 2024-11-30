using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Validation.Base
{
    public interface IValidator
    {
        abstract bool Validate();
    }
}
