using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Services.Interfaces
{
    public interface ILangSupportService
    {
        Task<Dictionary<string, string>> LoadLanguageAsync(int languageId);
    }
}
