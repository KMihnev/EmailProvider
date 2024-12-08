using EmailProvider.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.SearchData
{
    public class SearchConditionDate : SearchCondition
    {
        public SearchConditionDate(SearchTypeDate eDateSubType, string value)
        {
            SearchType = Enums.SearchType.SearchTypeDate;
            SearchSubType = (int)eDateSubType;
            SearchValue = value;
        }
    }

    public class SearchConditionEmail : SearchCondition
    {
        public SearchConditionEmail(SearchTypeEmail eSearchConditionEmail, string value)
        {
            SearchType = Enums.SearchType.SearchTypeEmail;
            SearchSubType = (int)eSearchConditionEmail;
            SearchValue = value;
        }
    }
}
