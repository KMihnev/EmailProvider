using EmailProvider.Logging;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.Validation;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmailProviderServer.TCP_Server.Dispatches.Countries
{
    public class GetCountriesDispatch : BaseDispatchHandler
    {
        private readonly CountryService _countryService;

        public GetCountriesDispatch(CountryService countryService)
        {
            _countryService = countryService;
            response = new EmailProvider.Reponse.Response();
        }

        public async override Task<bool> Execute(JsonElement parameters)
        {
            List<Country> countries = new List<Country>();

            try
            {
                countries = _countryService.GetAll().ToList();
                response.Data = countries;
            }
            catch (Exception)
            {
                response.bSuccess = false;
                response.msgError = LogMessages.DispatchErrorGetCountries;
                Logger.Log(LogMessages.DispatchErrorGetCountries, EmailProvider.Enums.LogType.LogTypeLog, EmailProvider.Enums.LogSeverity.Error);
                return false;
            }

            return true;
        }
    }
}
