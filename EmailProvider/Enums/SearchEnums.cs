//Includes

namespace EmailProvider.Enums
{
    //------------------------------------------------------
    //	SearchType
    //------------------------------------------------------
    public enum SearchType
    {
        SearchTypeDate,
        SearchTypeEmail
    }

    //------------------------------------------------------
    //	SearchTypeDate
    //------------------------------------------------------
    public enum SearchTypeDate
    {
        SearchTypeDateBefore,
        SearchTypeDateAfter,
    }

    //------------------------------------------------------
    //	SearchTypeEmail
    //------------------------------------------------------
    public enum SearchTypeEmail
    {
        SearchTypeEmailReceiver,
        SearchTypeEmailSender,
    }

    //------------------------------------------------------
    //	SearchTypeFolder
    //------------------------------------------------------
    public enum SearchTypeFolder
    {
        SearchTypeFolderDrafts,
        SearchTypeFolderIncoming,
        SearchTypeFolderOutgoing,
        SearchTypeFolderAll,
        SearchTypeFolderCustomCategory,
    }
}
