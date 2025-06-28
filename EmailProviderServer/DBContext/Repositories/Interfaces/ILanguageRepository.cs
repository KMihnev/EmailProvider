using EmailProvider.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAllLanguagesAsync();
        Task<Language?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
