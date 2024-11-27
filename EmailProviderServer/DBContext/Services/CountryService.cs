using AutoMapper;
using EmailProviderServer.DBContext.Repositories;
using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailServiceIntermediate.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmailProviderServer.DBContext.Services
{
    public class CountryService : ICountryService
    {
        private readonly CountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(CountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>(int? nCount = null)
        {
            IQueryable<Country> query = _countryRepository.All();

            if (nCount.HasValue)
                query = query.Take(nCount.Value);

            var countries = query.ToList();

            return _mapper.Map<IEnumerable<T>>(countries);
        }

        public T GetById<T>(int nId)
        {
            var country = _countryRepository
                .All()
                .FirstOrDefault(x => x.Id == nId);

            return _mapper.Map<T>(country);
        }

        public T GetByName<T>(string strName)
        {
            var country = _countryRepository
                .All()
                .FirstOrDefault(x => x.Name == strName);

            return _mapper.Map<T>(country);
        }

        public T GetByPhoneCode<T>(string strPhoneCode)
        {
            var country = _countryRepository
                .All()
                .FirstOrDefault(x => x.PhoneNumberCode == strPhoneCode);

            return _mapper.Map<T>(country);
        }
    }
}
