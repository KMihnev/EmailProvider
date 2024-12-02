using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmailProviderServer.DBContext.Services.Base;

namespace EmailProviderServer.TCP_Server.Dispatches.Countries
{
    public class GetCountriesDispatch : BaseDispatchHandler
    {
        private readonly ICountryService _countryService;

        public GetCountriesDispatch(ICountryService countryService)
        {
            _countryService = countryService;
        }

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
