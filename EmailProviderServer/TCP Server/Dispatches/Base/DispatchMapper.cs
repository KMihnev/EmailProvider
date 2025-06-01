//Includes
using EmailServiceIntermediate.Enums;
using EmailProviderServer.TCP_Server.Dispatches.Countries;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using EmailProviderServer.TCP_Server.Dispatches.Emails;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    //------------------------------------------------------
    //	DispatchMapper
    //------------------------------------------------------
    public class DispatchMapper
    {
        private readonly IServiceProvider _serviceProvider;

        //Constructor
        public DispatchMapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //Methods
        public BaseDispatchHandler MapDispatch(int dispatchCode)
        {
            switch ((DispatchEnums)dispatchCode)
            {
                case    DispatchEnums.Register:            return ActivatorUtilities.CreateInstance<RegisterDispatch>              (_serviceProvider);
                case    DispatchEnums.SetUpProfile:        return ActivatorUtilities.CreateInstance<SetUpProfileDispatch>          (_serviceProvider);
                case    DispatchEnums.GetCountries:        return ActivatorUtilities.CreateInstance<GetCountriesDispatch>          (_serviceProvider);
                case    DispatchEnums.Login:               return ActivatorUtilities.CreateInstance<LoginDispatch>                 (_serviceProvider);
                case    DispatchEnums.SendEmail:           return ActivatorUtilities.CreateInstance<SaveEmailDispatchS>            (_serviceProvider);
                case    DispatchEnums.GetEmail:            return ActivatorUtilities.CreateInstance<GetEmailDispatchS>             (_serviceProvider);
                case    DispatchEnums.DeleteEmails:        return ActivatorUtilities.CreateInstance<DeleteEmailDispatchS>          (_serviceProvider);
                case    DispatchEnums.ReceiveEmails:       return ActivatorUtilities.CreateInstance<ReceiveEmailDispatchS>         (_serviceProvider);
                case    DispatchEnums.LoadOutgoingEmails:  return ActivatorUtilities.CreateInstance<LoadOutgoingEmailsDispatchS>   (_serviceProvider);
                case    DispatchEnums.LoadIncomingEmails:  return ActivatorUtilities.CreateInstance<LoadIncomingEmailsDispatchS>   (_serviceProvider);
                case    DispatchEnums.LoadDrafts:          return ActivatorUtilities.CreateInstance<LoadDraftEmailsDispatchS>      (_serviceProvider);
                case    DispatchEnums.LoadFolders:         return ActivatorUtilities.CreateInstance<LoadFoldersDispatchS>          (_serviceProvider);
                case    DispatchEnums.LoadEmailsByFolder:  return ActivatorUtilities.CreateInstance<LoadFolderEmailsDispatchS>     (_serviceProvider);
                default:
                    throw new NotImplementedException($"Dispatch code {dispatchCode} is not supported.");
            }
        }
    }
}
