using EmailProvider.Dispatches;
using EmailProvider.Enums;
using EmailProvider.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Dispatches.Base;
using EMailProviderClient.Controllers.UserControl;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMailProviderClient.Dispatches.Countries
{
    public class CountriesDispatchC
    {
        public static async Task<bool> LoadCountries(List<CountrySerializable> countries)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                InPackage.Serialize((int)DispatchEnums.GetCountries);

                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();
                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    return false;
                }

                List<CountrySerializable> deserializedCountries = null;
                OutPackage.Deserialize(out deserializedCountries);

                if (deserializedCountries == null)
                {
                    Logger.LogError(string.Format(LogMessages.NullObject, deserializedCountries.GetType().Name));
                    return false;
                }

                if (deserializedCountries.Count <= 0)
                    Logger.LogWarning(LogMessages.NoCountriesLoaded);

                countries.AddRange(deserializedCountries);

                return true;
            }
            catch (Exception)
            {
                Logger.LogErrorCalling();
                return false;
            }
        }
    }
}
