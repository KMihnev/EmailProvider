//Includes

namespace EmailProviderServer.DBContext.Services
{
    public interface ICategoryService
    {
        IEnumerable<T> GetAll<T>(int? nCount = null);

        T GetById<T>(string nId);

        T GetByUserId<T>(string nId);

        T GetByName<T>(string strName);
    }
}
