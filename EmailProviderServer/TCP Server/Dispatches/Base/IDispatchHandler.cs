using EmailServiceIntermediate.Dispatches;
using System.Text.Json;

namespace EmailProviderServer.TCP_Server.Dispatches.Interfaces
{
    public abstract class BaseDispatchHandler
    {
        public string errorMessage { get; set; }
        public abstract Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage);
    }
}
