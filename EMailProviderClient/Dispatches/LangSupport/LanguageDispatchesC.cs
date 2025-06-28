using EmailProvider.Models.Serializables;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models;
using EMailProviderClient.Dispatches.Base;
using EmailProvider.Models.DBModels;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;

namespace EMailProviderClient.Dispatches.Lang
{
    public static class LanguageDispatchesC
    {
        /// <summary> RPC за зареждане на всички налични езици </summary>
        public static async Task<List<LanguageDto>> LoadLanguages()
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                InPackage.Serialize((int)DispatchEnums.LoadLanguages);

                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                    return null;

                List<LanguageDto> languages = null;
                OutPackage.Deserialize(out languages);

                return languages;
            }
            catch (Exception)
            {
                Logger.LogErrorCalling();
                return null;
            }
        }
    }
}
