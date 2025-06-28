using EmailServiceIntermediate.Dispatches;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.DBContext.Services.Interfaces;
using EmailProviderServer.DBContext.Services;
using EmailServiceIntermediate.Logging;
using EmailProvider.Models.Serializables;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class GetLangSupportDispatchS : BaseDispatchHandler
    {
        private readonly ILangSupportService _localizationService;

        protected override bool RequiresSession => false;

        public GetLangSupportDispatchS(ILangSupportService localizationService)
        {
            _localizationService = localizationService;
        }

        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            int languageId;
            try
            {
                InPackage.Deserialize(out languageId);
            }
            catch (Exception)
            {
                Logger.LogError(LogMessages.InteralError);
                return false;
            }

            try
            {
                var translations = await _localizationService.LoadLanguageAsync(languageId);
                OutPackage.Serialize(true);
                OutPackage.Serialize(translations);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.InteralError);
                return false;
            }
        }
    }
}
