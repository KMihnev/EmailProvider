using EmailProvider.Dispatches;
using EmailProvider.Enums;
using EmailProvider.Logging;
using EMailProviderClient.UserControl;
using EmailServiceIntermediate.Models;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EMailProviderClient.Dispatches
{
    public class CountriesDispatchC
    {
        public static async Task<bool> LoadCountries(List<Country> countries)
        {
            try
            {
                var request = new MethodRequest
                {
                    eDispatch = DispatchEnums.GetCountries,
                    Parameters = JsonDocument.Parse(JsonSerializer.Serialize(new { countries })).RootElement
                };
                DispatchHandlerC.InitClient();

                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                await dispatchHandlerC.SendRequest(request);

                if (!await dispatchHandlerC.HandleResponse())
                    return false;

                if (dispatchHandlerC.Response.Data != null)
                {
                    var newCountries = JsonSerializer.Deserialize<List<Country>>(dispatchHandlerC.Response.Data.ToString());
                    if (newCountries != null)
                    {
                        countries.Clear();
                        foreach (var country in newCountries)
                        {
                            countries.Add(country);
                        }
                    }
                }

                if(countries.Count() <=0)
                    Logger.Log(LogMessages.NoCountriesLoaded, LogType.LogTypeScreen, LogSeverity.Error);

                return true;

            }
            catch (Exception)
            {
                Logger.Log(LogMessages.DispatchErrorRegister, LogType.LogTypeScreen, LogSeverity.Error);
                return false;
            }
        }
    }
}
