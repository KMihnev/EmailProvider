//Includes

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IUserService
    {
        IEnumerable<T> GetAll<T>(int? nCount = null);

        IEnumerable<T> GetAllByCountryId<T>(int nId, int? count = null);

        T GetById<T>(int nId);

        T GetByName<T>(string strName);

        T GetByEmail<T>(string strEmail);
    }
}
