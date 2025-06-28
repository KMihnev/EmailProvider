using EmailServiceIntermediate.Dispatches;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.DBContext.Services.Interfaces;
using EmailProviderServer.DBContext.Services;
using EmailServiceIntermediate.Logging;
using EmailProvider.Models.Serializables;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class GetLanguagesDispatchS : BaseDispatchHandler
    {
        private readonly ILanguageService _languageService;

        public GetLanguagesDispatchS(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            try
            {
                var languages = await _languageService.GetAllAsync<LanguageDto>();
                OutPackage.Serialize(true);
                OutPackage.Serialize(languages.ToList());
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
