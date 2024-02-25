//Includes

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface ICategoryService
    {
        IEnumerable<T> GetAll<T>(int? nCount = null);

        IEnumerable<T> GetAllByUserId<T>(int nId, int? nCount = null);

        T GetById<T>(int nId);

        T GetByName<T>(string strName);
    }
}
