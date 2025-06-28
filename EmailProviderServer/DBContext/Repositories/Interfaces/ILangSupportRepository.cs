using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    public interface ILangSupportRepository
    {
        Task<Dictionary<string, string>> GetLocalizedStringsAsync(int languageId);
    }
}
