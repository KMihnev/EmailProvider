//Includes

using AutoMapper;
using EmailServiceIntermediate.Logging;
using EmailProviderServer.DBContext.Repositories;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using EmailProviderServer.DBContext.Repositories.Interfaces;

namespace EmailProviderServer.DBContext.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _oUserRepositoryS;
        private readonly IMapper _mapper;

        public UserService(IUserRepository oUserRepository, IMapper mapper)
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

        public async Task<T> UpdateAsync<T>(User user)
        {
            if (user == null)
                Logger.Log(LogMessages.UserNotFound, EmailServiceIntermediate.Enums.LogType.LogTypeLog, EmailServiceIntermediate.Enums.LogSeverity.Error);

            _oUserRepositoryS.Update(user);

            await _oUserRepositoryS.SaveChangesAsync();

            return _mapper.Map<T>(user);
        }
    }
}
