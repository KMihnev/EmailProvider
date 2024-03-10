//Includes

using AutoMapper;
using EmailProvider.Logging;
using EmailProviderServer.DBContext.Repositories;
using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<User> GetAll(int? nCount = null)
        {
            IQueryable<User> oQuery = this.oUserRepositoryS
               .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public IEnumerable<User> GetAllByCountryId(int nId, int? nCount = null)
        {
            IQueryable<User> oQuery = this.oUserRepositoryS
               .All().Where(u => u.CountryId == nId);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public User GetByEmail(string strEmail)
        {
            var oUser = this.oUserRepositoryS
            .All()
                .Where(u => u.Email == strEmail)
                .FirstOrDefault();
            return oUser;
        }

        public User GetById(int nId)
        {
            var oUser = this.oUserRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .FirstOrDefault();
            return oUser;
        }

        public User GetByName(string strName)
        {
            var oUser = this.oUserRepositoryS
                 .All()
                 .Where(u => u.Name == strName)
                 .FirstOrDefault();
            return oUser;
        }

        public async Task CreateAsync(User user)
        {

            await oUserRepositoryS.AddAsync(user);

            await oUserRepositoryS.SaveChangesAsync();
        }

        public async Task UpdateAsync(int nId,User user)
        {
            if (user == null)
                Logger.Log(LogMessages.UserNotFound, EmailProvider.Enums.LogType.LogTypeLog, EmailProvider.Enums.LogSeverity.Error);

            oUserRepositoryS.Update(user);

            await oUserRepositoryS.SaveChangesAsync();
        }
    }
}
