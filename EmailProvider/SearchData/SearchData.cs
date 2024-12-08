using EmailProvider.Enums;
using EmailServiceIntermediate.Enums;
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

        public SearchTypeFolder SearchTypeFolder { get; set; }

        public int UdfFolderID { get; set; }

        public List<SearchCondition> Conditions {  get; set; }

        public SearchData()
        {
            Conditions = new List<SearchCondition>();
        }

        public void Clear()
        {
            Conditions.Clear();
        }

        public void AddCondition(SearchCondition searchCondition)
        {
            Conditions.Add(searchCondition);
        }

        public int GetSearchTypeFolder()
        {
            SearchTypeFolder searchType;

            if ((SearchTypeFolder == SearchTypeFolder.SearchTypeFolderDrafts))
                searchType = SearchTypeFolder.SearchTypeFolderOutgoing;
            else if ((SearchTypeFolder == SearchTypeFolder.SearchTypeFolderCustomCategory))
                searchType = SearchTypeFolder.SearchTypeFolderAll;
            else 
                searchType = SearchTypeFolder;

            return (int)searchType;
        }

        public string ConstructWhereClause()
        {
            var WhereConditions = new List<string>();

            int statusDraft = EmailStatusProvider.GetDraftStatus();
            string statusCondition;


            if (SearchTypeFolder == SearchTypeFolder.SearchTypeFolderDrafts)
                statusCondition = $"M.STATUS = {statusDraft}";
            else
                statusCondition = $"M.STATUS <> {statusDraft}";

            WhereConditions.Add(statusCondition);

            foreach (var condition in Conditions)
            {
                switch (condition.SearchType)
                {
                    case SearchType.SearchTypeDate:
                        if (DateTime.TryParse(condition.SearchValue, out DateTime dateVal))
                        {
                            var dateCondition = (SearchTypeDate)condition.SearchSubType switch
                            {
                                SearchTypeDate.SearchTypeDateBefore => $"M.DATE_OF_COMPLETION < '{dateVal:yyyy-MM-dd HH:mm:ss}'",
                                SearchTypeDate.SearchTypeDateAfter => $"M.DATE_OF_COMPLETION > '{dateVal:yyyy-MM-dd HH:mm:ss}'",
                                _ => null
                            };
                            if (!string.IsNullOrEmpty(dateCondition))
                                WhereConditions.Add(dateCondition);
                        }
                        break;

                    case SearchType.SearchTypeEmail:
                        var emailCondition = (SearchTypeEmail)condition.SearchSubType switch
                        {
                            SearchTypeEmail.SearchTypeEmailSender => $"SI.SenderEmail = '{condition.SearchValue}'",
                            SearchTypeEmail.SearchTypeEmailReceiver => $"RI.ReceiverEmails LIKE '%{condition.SearchValue}%'",
                            _ => null
                        };
                        if (!string.IsNullOrEmpty(emailCondition))
                            WhereConditions.Add(emailCondition);
                        break;
                }
            }

            string WhereStatement = "";
            if (WhereConditions.Count > 0)
            {
                WhereStatement = string.Join(" AND ", WhereConditions);
            }

            return WhereStatement;
        }
    }
}
