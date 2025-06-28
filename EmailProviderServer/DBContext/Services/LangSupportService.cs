using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProviderServer.DBContext.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmailProviderServer.DBContext.Services.LangSupportService;

namespace EmailProviderServer.DBContext.Services
{
    public class LangSupportService : ILangSupportService
    {
        private readonly ILangSupportRepository _repository;
    
        public LangSupportService(ILangSupportRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<Dictionary<string, string>> LoadLanguageAsync(int languageId)
        {
            return await _repository.GetLocalizedStringsAsync(languageId);
        }
    } 
}
