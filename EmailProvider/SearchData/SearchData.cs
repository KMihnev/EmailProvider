//Includes
using EmailProvider.Enums;
using EmailServiceIntermediate.Enums;

namespace EmailProvider.SearchData
{
    public class SearchData
    {
        public SearchData()
        {

        }

        public int UserId { get; set; }

        public List<SearchCondition> Conditions { get; set; } = new();

        public void Clear()
        {
            Conditions.Clear();
        }

        public void AddCondition(SearchCondition searchCondition)
        {
            Conditions.Add(searchCondition);
        }
    }
}
