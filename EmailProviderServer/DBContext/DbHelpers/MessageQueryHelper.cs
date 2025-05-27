using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.DbHelpers
{
    public class SearchLoadOptions
    {
        public bool IncludeRecipients { get; set; } = false;
        public bool IncludeFiles { get; set; } = false;
        public bool IncludeUserMessages { get; set; } = false;
    }

    public static class QueryIncludeHelper
    {
        public static IQueryable<Message> ApplyIncludes(IQueryable<Message> query, SearchLoadOptions options)
        {
            if (options.IncludeRecipients)
                query = query.Include(m => m.MessageRecipients);
            if (options.IncludeFiles)
                query = query.Include(m => m.Files);
            if (options.IncludeUserMessages)
                query = query.Include(m => m.UserMessages);

            return query;
        }
    }
}
