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

                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();
                if(!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    return false;
                }

                OutPackage.Deserialize(out countries);

                if(countries == null)
                    return false;

                if (countries.Count <= 0)
                    Logger.Log(LogMessages.NoCountriesLoaded, LogType.LogTypeScreen, LogSeverity.Warning);

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
