//Includes

using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailProviderServer.Models;
using EmailServiceIntermediate.Mapping;

namespace EmailProviderServer.DBContext.Services
{

    public class CountryService : ICountryService
    {

        private readonly IRepositoryS<Country> oCountryRepositoryS;

        public CountryService(IRepositoryS<Country> oCountryRepositoryS)
        {
            this.oCountryRepositoryS = oCountryRepositoryS;
        }

        public IEnumerable<T> GetAll<T>(int? nCount = null)
        {
            IQueryable<Country> oQuery = this.oCountryRepositoryS
                .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.To<T>().ToList();
        }

        public T GetById<T>(int nId)
        {
            var Country = this.oCountryRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .To<T>().FirstOrDefault();
            return Country;
        }

        public T GetByName<T>(string strName)
        {
            var oCountry = this.oCountryRepositoryS
                .All()
                .Where(x => x.Name == strName)
                .To<T>().FirstOrDefault();
            return oCountry;
        }

        public T GetByPhoneCode<T>(string strPhoneCode)
        {
            var oCountry = this.oCountryRepositoryS
                .All()
                .Where(x => x.PhoneNumberCode == strPhoneCode)
                .To<T>().FirstOrDefault();
            return oCountry;
        }
    }
}
