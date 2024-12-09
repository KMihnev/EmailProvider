//Includes
using EmailProviderServer.DBContext.Repositories.Base;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    //------------------------------------------------------
    //	ICountryRepository
    //------------------------------------------------------
    public interface ICountryRepository : IRepositoryS<Country>
    {
        Task<Country> GetByName(string name);
        Task<Country> GetByPhoneCode(string code);
    }
}
