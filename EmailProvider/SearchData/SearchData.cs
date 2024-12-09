//Includes
using EmailProvider.Enums;
using EmailServiceIntermediate.Enums;

namespace EmailProvider.SearchData
{
    //------------------------------------------------------
    //	SearchData
    //------------------------------------------------------

    /// <summary> Клас за филтиране на имейли по много кондиции </summary>
    public class SearchData
    {
        /// <summary> Идентификатор на потребител чиито имейли ще филтрираме </summary>
        public int UserID { get; set; }

        /// <summary> Тип на папка в която ще търсим </summary>
        public SearchTypeFolder SearchTypeFolder { get; set; }

        /// <summary> Ако типа на папката е лична, ще имаме и нейното ID </summary>
        public int UdfFolderID { get; set; }

        /// <summary> Списък от кондиции </summary>
        public List<SearchCondition> Conditions {  get; set; }

        //Constructor
        public SearchData()
        {
            Conditions = new List<SearchCondition>();
        }
        //Methods
        /// <summary> Зачистваме кондициите </summary>
        public void Clear()
        {
            Conditions.Clear();
        }

        /// <summary> Добавяне на кондиция </summary>
        public void AddCondition(SearchCondition searchCondition)
        {
            Conditions.Add(searchCondition);
        }

        /// <summary> Връща папка в която ще търсим </summary>
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

        /// <summary> Конструира Where клауза което ще се използва в процедурата по търсене </summary>
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
