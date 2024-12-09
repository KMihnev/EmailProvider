//Includes
using EmailProvider.Enums;

namespace EmailProvider.SearchData
{
    //------------------------------------------------------
    //	SearchConditionDate
    //------------------------------------------------------

    /// <summary> Типизирана кондиция </summary>
    public class SearchConditionDate : SearchCondition
    {
        //Constructor
        public SearchConditionDate(SearchTypeDate eDateSubType, string value)
        {
            SearchType = Enums.SearchType.SearchTypeDate;
            SearchSubType = (int)eDateSubType;
            SearchValue = value;
        }
    }

    //------------------------------------------------------
    //	SearchConditionEmail
    //------------------------------------------------------

    /// <summary> Типизирана кондиция </summary>
    public class SearchConditionEmail : SearchCondition
    {
        //Constructor
        public SearchConditionEmail(SearchTypeEmail eSearchConditionEmail, string value)
        {
            SearchType = Enums.SearchType.SearchTypeEmail;
            SearchSubType = (int)eSearchConditionEmail;
            SearchValue = value;
        }
    }
}
