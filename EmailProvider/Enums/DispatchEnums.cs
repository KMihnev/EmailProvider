using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Enums
{
    public enum DispatchEnums : short
    {
        Empty = 0,
        Login,
        Register,
        SetUpProfile,
        GetCountries,
    }

    public enum Tables
    {
        Countries = 0,
        Users,
    }
}
