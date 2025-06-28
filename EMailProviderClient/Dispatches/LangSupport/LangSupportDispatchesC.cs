using EmailProvider.Models.Serializables;
using EmailServiceIntermediate.Logging;
using EMailProviderClient.Dispatches.Base;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;

namespace EMailProviderClient.Dispatches.Lang
{
    public static class LangSupportDispatchesC
    {
        public static async Task<Dictionary<string, string>> LoadTranslations(int languageId)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                InPackage.Serialize((int)DispatchEnums.LoadLangSupport);
                InPackage.Serialize(languageId);

                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                    return null;

                Dictionary<string, string> translations = null;
                OutPackage.Deserialize(out translations);

                return translations;
            }
            catch (Exception)
            {
                Logger.LogErrorCalling();
                return null;
            }
        }
    }
}
