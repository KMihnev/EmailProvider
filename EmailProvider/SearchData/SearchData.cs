//Includes
using EmailProvider.Enums;
using EmailServiceIntermediate.Enums;

namespace EmailProvider.SearchData
{
    public class SearchData
    {
        public SearchData()
        {
            Take = 10;
            OrderBy = OrderBy.OrderByAscending;
        }

        public int Skip { get; set; }

        public int Take { get; set; }
        public List<SearchCondition> Conditions { get; set; } = new();

        public OrderBy OrderBy { get; set; }

        public string? Keyword { get; set; }

        public void Clear()
        {
            Conditions.Clear();
        }

        public void AddCondition(SearchCondition searchCondition)
        {
            Conditions.Add(searchCondition);
        }

        public void RemoveCondition(SearchType eSearchType)
        {
            Conditions.RemoveAll(x => x.SearchType == eSearchType);
        }
    }
}
