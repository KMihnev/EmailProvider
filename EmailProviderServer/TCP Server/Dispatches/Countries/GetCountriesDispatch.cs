//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.DBContext.Services.Base;

namespace EmailProviderServer.TCP_Server.Dispatches.Countries
{
    //------------------------------------------------------
    //	GetCountriesDispatch
    //------------------------------------------------------
    public class GetCountriesDispatch : BaseDispatchHandler
    {
        private readonly ICountryService _countryService;

        //Constructor
        public GetCountriesDispatch(ICountryService countryService)
        {
            _countryService = countryService;
        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            try
            {
                List<CountrySerializable> countries = new List<CountrySerializable>( await _countryService.GetAllAsync<CountrySerializable>() );

                OutPackage.Serialize(true);
                OutPackage.Serialize(countries);
            }
            catch (Exception)
            {
                errorMessage = LogMessages.InteralError;
                return false;
            }

            return true;
        }
    }
}
