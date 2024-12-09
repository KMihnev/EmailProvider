//Includes

using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    //------------------------------------------------------
    //	ICategoryService
    //------------------------------------------------------
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll(int? nCount = null);

        Category GetById(int nId);

        Category GetByName(string strName);
    }
}
