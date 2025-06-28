using EmailProvider.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(int id);
        Task<bool> IsValidLanguageIdAsync(int id);
    }
}
