using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Enums
{
    public enum EmailDirections : short
    {
        EmailDirectionIn = 0,
        EmailDirectionOut = 1,
    }

    public enum EmailStatuses : short
    {
        EmailStatusNew = 0,
        EmailStatusDraft = 1,
        EmailStatusComplete = 2,
    }
}
