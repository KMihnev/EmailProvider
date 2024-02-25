//Includes

namespace EmailProviderServer.DBContext.Services
{
    public interface ICountryService
    {
        IEnumerable<T> GetAll<T>(int? nCount = null);

        T GetById<T>(int nId);

        T GetByName<T>(string strName);

        T GetByPhoneCode<T>(string strPhoneCode);
    }
}
