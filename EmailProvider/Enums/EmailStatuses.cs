//Includes

namespace EmailServiceIntermediate.Enums
{

    //------------------------------------------------------
    //	EmailStatusProvider
    //------------------------------------------------------
    public class EmailStatusProvider
    {
        //------------------------------------------------------
        //	EmailStatuses
        //------------------------------------------------------
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
