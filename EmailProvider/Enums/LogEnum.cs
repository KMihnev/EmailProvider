using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Enums
{
    public enum LogType
    {
        LogTypeNine = 0,
        LogTypeScreen,
        LogTypeLog,
        LogTypeScreenLog,
    }

    public enum LogSeverity
    {
        Info = 0,
        Warning,
        Error,
    }
}
