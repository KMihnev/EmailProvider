//Includes

using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface ICountryService
    {
        IEnumerable<Country> GetAll(int? nCount = null);

        Country GetById(int nId);

        Country GetByName(string strName);

        Country GetByPhoneCode(string strPhoneCode);
    }
}
