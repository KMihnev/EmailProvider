﻿//Includes
using AutoMapper;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Logging;
using Microsoft.EntityFrameworkCore;

namespace EmailProviderServer.DBContext.Services
{
    //------------------------------------------------------
    //	CountryService
    //------------------------------------------------------
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        //Constructor
        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        //Methods
        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var query = _countryRepository.AllAsNoTracking().OrderBy(c => c.Name);

            var countries = await query.ToListAsync();

            return _mapper.Map<IEnumerable<T>>(countries);
        }

        public async Task<T> GetByIdAsync<T>(int nId)
        {
            var country = await _countryRepository.GetByID(nId);

            if (country == null)
            {
                Logger.LogError(LogMessages.RecordNotFound, this.GetType().Name, nId);
            }

            return _mapper.Map<T>(country);
        }

        public async Task<T> GetByNameAsync<T>(string strName)
        {
            var country = await _countryRepository.GetByName(strName);

            if (country == null)
            {
                Logger.LogError(LogMessages.RecordNotFound, this.GetType().Name, strName);
            }

            return _mapper.Map<T>(country);
        }

        public async Task<T> GetByPhoneCodeAsync<T>(string strPhoneCode)
        {
            var country = await _countryRepository.GetByPhoneCode(strPhoneCode);

            if (country == null)
            {
                Logger.LogError(LogMessages.RecordNotFound, this.GetType().Name, strPhoneCode);
            }

            return _mapper.Map<T>(country);
        }
    }
}
