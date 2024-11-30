using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceIntermediate.Enums
{

    public class EmailStatusProvider
    {
        private enum EmailStatuses : int
        {
            EmailStatusNew = 0,
            EmailStatusDraft = 1,
            EmailStatusComplete = 2,
        }

        public static int GetDraftStatus()
        {
            return (int)EmailStatuses.EmailStatusDraft;
        }

        public static int GetNewStatus()
        {
            return (int)EmailStatuses.EmailStatusNew;
        }

        public static int GetCompleteStatus()
        {
            return (int)EmailStatuses.EmailStatusComplete;
        }
    }
}
