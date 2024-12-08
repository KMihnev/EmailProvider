using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.SearchData
{
    public class SearchData
    {
        public int UserID { get; set; }
        public List<SearchCondition> Conditions {  get; set; }

        public SearchData()
        {
            Conditions = new List<SearchCondition>();
        }

        public void AddCondition(SearchCondition condition)
        {
            Conditions.Add(condition);
        }

        public void Clear()
        {
            Conditions.Clear();
        }
    }
}
