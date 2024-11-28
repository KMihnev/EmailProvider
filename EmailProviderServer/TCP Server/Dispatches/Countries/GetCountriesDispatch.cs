using EmailProvider.Dispatches;
using EmailProvider.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailProviderServer.TCP_Server.Dispatches.Countries
{
    public class GetCountriesDispatch : BaseDispatchHandler
    {
        private readonly CountryService _countryService;

        public GetCountriesDispatch(CountryService countryService)
        {
            _countryService = countryService;
        }

        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            try
            {
                List<CountrySerializable> countries = _countryService.GetAll<CountrySerializable>().ToList();

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
