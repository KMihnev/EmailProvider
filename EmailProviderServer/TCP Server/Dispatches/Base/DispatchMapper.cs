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
                case DispatchEnums.Register:
                    return ActivatorUtilities.CreateInstance<RegisterDispatch>(_serviceProvider);
                case DispatchEnums.SetUpProfile:
                    return ActivatorUtilities.CreateInstance<SetUpProfileDispatch>(_serviceProvider);
                case DispatchEnums.GetCountries:
                    return ActivatorUtilities.CreateInstance<GetCountriesDispatch>(_serviceProvider);
                case DispatchEnums.Login:
                    return ActivatorUtilities.CreateInstance<LoginDispatch>(_serviceProvider);
                case DispatchEnums.SendEmail:
                    return ActivatorUtilities.CreateInstance<SaveEmailDispatchS>(_serviceProvider);
                case DispatchEnums.LoadEmails:
                    return ActivatorUtilities.CreateInstance<LoadEmailsDispatchS>(_serviceProvider);
                case DispatchEnums.GetEmail:
                    return ActivatorUtilities.CreateInstance<GetEmailDispatchS>(_serviceProvider);
                case DispatchEnums.DeleteEmails:
                    return ActivatorUtilities.CreateInstance<DeleteEmailDispatchS>(_serviceProvider);
                default:
                    throw new NotImplementedException($"Dispatch code {dispatchCode} is not supported.");
            }
        }
    }
}
