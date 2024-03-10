//Includes

using EmailServiceIntermediate.Models;
using System.Collections.Generic;

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IUserService
    {
        IEnumerable<User> GetAll(int? nCount = null);

        IEnumerable<User> GetAllByCountryId(int nId, int? count = null);

        User GetById(int nId);

        User GetByName(string strName);

        User GetByEmail(string strEmail);
    }
}
