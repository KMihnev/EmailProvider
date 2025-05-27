//Includes
using EmailProvider.Enums;

namespace EmailProvider.SearchData
{
    //------------------------------------------------------
    //	SearchCondition
    //------------------------------------------------------


    /// <summary> Клас за филтиране на данни по condition </summary>
    public class SearchCondition
    {
        /// <summary> Основен филтър </summary>
        public SearchType SearchType {  get; set; }

        /// <summary> Под филтър </summary>
        public int SearchSubType { get; set; }

        /// <summary> Търсена стойност </summary>
        public string SearchValue { get; set; }
    }
}
