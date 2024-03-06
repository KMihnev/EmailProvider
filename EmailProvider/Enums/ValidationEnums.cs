using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Enums
{
    public enum ValidationTypes
    {
        ValidationTypeNone = 0,
        ValidationTypeName,
        ValidationTypePassword,
        ValidationTypeEmail,
        ValidationTypePhoneNumber
    }
}
