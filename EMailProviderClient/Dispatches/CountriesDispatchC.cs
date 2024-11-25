using EmailProvider.Dispatches;
using EmailProvider.Enums;
using EmailProvider.Logging;
using EMailProviderClient.UserControl;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMailProviderClient.Dispatches
{
    public class CountriesDispatchC
    {
        public static async Task<bool> LoadCountries(List<Country> countries)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                InPackage.Serialize((int)DispatchEnums.GetCountries);
                InPackage.Serialize(countries);

                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();
                if(!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    return false;
                }

                bool bResponse = false;
                OutPackage.Deserialize(out bResponse);
                if (!bResponse)
                {
                    return false;
                }

                OutPackage.Deserialize(out List<Country> newCountries);

                if (newCountries != null)
                {
                    countries.Clear();
                    countries.AddRange(newCountries);
                }

                if (countries.Count <= 0)
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
