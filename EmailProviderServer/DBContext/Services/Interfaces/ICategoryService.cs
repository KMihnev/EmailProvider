//Includes

using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll(int? nCount = null);

        IEnumerable<Category> GetAllByUserId(int nId, int? nCount = null);

        Category GetById(int nId);

        Category GetByName(string strName);
    }
}
