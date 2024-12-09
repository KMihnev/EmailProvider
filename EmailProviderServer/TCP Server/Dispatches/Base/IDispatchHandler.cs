//Includes
using EmailServiceIntermediate.Dispatches;

namespace EmailProviderServer.TCP_Server.Dispatches.Interfaces
{
    //------------------------------------------------------
    //	BaseDispatchHandler
    //------------------------------------------------------
    public abstract class BaseDispatchHandler
    {
        public string errorMessage { get; set; }
        public abstract Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage);
    }
}
