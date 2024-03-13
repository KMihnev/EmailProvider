//Includes

using EmailProviderServer.DBContext.Repositories;
using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services
{

    public class CountryService : ICountryService
    {

        private readonly CountryRepository oCountryRepositoryS;

        public CountryService(CountryRepository oCountryRepositoryS)
        {
            this.oCountryRepositoryS = oCountryRepositoryS;
        }

        public IEnumerable<Country> GetAll(int? nCount = null)
        {
            IQueryable<Country> oQuery = this.oCountryRepositoryS
                .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public Country GetById(int nId)
        {
            var Country = this.oCountryRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .FirstOrDefault();
            return Country;
        }

        public Country GetByName(string strName)
        {
            var oCountry = this.oCountryRepositoryS
                .All()
                .Where(x => x.Name == strName)
                .FirstOrDefault();
            return oCountry;
        }

        public Country GetByPhoneCode(string strPhoneCode)
        {
            var oCountry = this.oCountryRepositoryS
                .All()
                .Where(x => x.PhoneNumberCode == strPhoneCode)
                .FirstOrDefault();
            return oCountry;
        }
    }
}
