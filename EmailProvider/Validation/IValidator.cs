using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceIntermediate.Validation
{
    public interface IValidator
    {
        abstract bool Validate();
    }
}
