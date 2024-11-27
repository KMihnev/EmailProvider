//Includes

using AutoMapper;
using EmailProvider.Logging;
using EmailProviderServer.DBContext.Repositories;
using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;

namespace EmailProviderServer.DBContext.Services
{
    public class UserService : IUserService
    {

        private readonly UserRepository _oUserRepositoryS;
        private readonly IMapper _mapper;

        public UserService(UserRepository oUserRepository, IMapper mapper)
        {
            _oUserRepositoryS = oUserRepository;
            _mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>(int? nCount = null)
        {
            IQueryable<User> oQuery = this._oUserRepositoryS
               .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            var users = oQuery.ToList();

            return _mapper.Map<IEnumerable<T>>(users);
        }

        public IEnumerable<T> GetAllByCountryId<T>(int nId, int? nCount = null)
        {
            IQueryable<User> oQuery = this._oUserRepositoryS
               .All().Where(u => u.CountryId == nId);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            var users = oQuery.ToList();

            return _mapper.Map<IEnumerable<T>>(users);
        }

        public T GetByEmail<T>(string strEmail)
        {
            var oUser = this._oUserRepositoryS
            .All()
                .Where(u => u.Email == strEmail)
                .FirstOrDefault();

            return _mapper.Map<T>(oUser);
        }

        public T GetById<T>(int nId)
        {
            var oUser = this._oUserRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .FirstOrDefault();
            return _mapper.Map<T>(oUser);
        }

        public T GetByName<T>(string strName)
        {
            var oUser = this._oUserRepositoryS
                 .All()
                 .Where(u => u.Name == strName)
                 .FirstOrDefault();

            return _mapper.Map<T>(oUser);
        }

        public bool CheckIfExists(int nId)
        {
            var user = this._oUserRepositoryS.GetByID(nId);
            if (user == null) return false;

            return true;
        }

        public async Task<T> CreateAsync<T>(User user)
        {
            await _oUserRepositoryS.AddAsync(user);

            await _oUserRepositoryS.SaveChangesAsync();

            return _mapper.Map<T>(user);
        }

        public async Task<T> UpdateAsync<T>(int nId,User user)
        {
            if (user == null)
                Logger.Log(LogMessages.UserNotFound, EmailProvider.Enums.LogType.LogTypeLog, EmailProvider.Enums.LogSeverity.Error);

            _oUserRepositoryS.Update(user);

            await _oUserRepositoryS.SaveChangesAsync();

            return _mapper.Map<T>(user);
        }
    }
}
