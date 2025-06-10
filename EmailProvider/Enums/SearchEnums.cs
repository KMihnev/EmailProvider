//Includes

namespace EmailProvider.Enums
{
    //------------------------------------------------------
    //	SearchType
    //------------------------------------------------------
    public enum SearchType
    {
        SearchTypeDate,
        SearchTypeEmail,
        SearchTypeDeleted,
        SearchTypeRead,
        SearchTypeUnread,
    }

    //------------------------------------------------------
    //	SearchTypeDate
    //------------------------------------------------------
    public enum SearchTypeDate
    {
        SearchTypeDateBefore,
        SearchTypeDateAfter,
    }

    public enum OrderBy
    {
        OrderByAscending,
        OrderByDescending,
    }
}
