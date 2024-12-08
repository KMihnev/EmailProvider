using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Enums
{
    public enum SearchType
    {
        SearchTypeDate,
        SearchTypeEmail
    }

    public enum SearchTypeDate
    {
        SearchTypeDateBefore,
        SearchTypeDateAfter,
    }

    public enum SearchTypeEmail
    {
        SearchTypeEmailReceiver,
        SearchTypeEmailSender,
    }

    public enum SearchTypeFolder
    {
        SearchTypeFolderDrafts,
        SearchTypeFolderIncoming,
        SearchTypeFolderOutgoing,
        SearchTypeFolderAll,
        SearchTypeFolderCustomCategory,
    }
}
