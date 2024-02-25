//Includes

using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailProviderServer.Models;
using EmailServiceIntermediate.Mapping;
using System.Security.Cryptography;

namespace EmailProviderServer.DBContext.Services
{
    public class UserService : IUserService
    {

        private readonly IRepositoryS<User> oUserRepositoryS;

        public UserService(IRepositoryS<User> oUserRepository)
        {
            this.oUserRepositoryS = oUserRepository;
        }

        public IEnumerable<T> GetAll<T>(int? nCount = null)
        {
            IQueryable<User> oQuery = this.oUserRepositoryS
               .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByCountryId<T>(int nId, int? nCount = null)
        {
            IQueryable<User> oQuery = this.oUserRepositoryS
               .All().Where(u => u.CountryId == nId);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.To<T>().ToList();
        }

        public T GetByEmail<T>(string strEmail)
        {
            var oUser = this.oUserRepositoryS
            .All()
                .Where(u => u.Email == strEmail)
                .To<T>().FirstOrDefault();
            return oUser;
        }

        public T GetById<T>(int nId)
        {
            var oUser = this.oUserRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .To<T>().FirstOrDefault();
            return oUser;
        }

        public T GetByName<T>(string strName)
        {
            var oUser = this.oUserRepositoryS
                 .All()
                 .Where(u => u.Name == strName)
                 .To<T>().FirstOrDefault();
            return oUser;
        }
    }
}
