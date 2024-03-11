using EmailProvider.Dispatches;
using EmailProvider.Reponse;
using System.Text.Json;

namespace EmailProviderServer.TCP_Server.Dispatches.Interfaces
{
    public interface IDispatchHandler
    {
        Response response { get; set; } 
        abstract Task<bool> Execute(JsonElement parameters);
    }

    public class BaseDispatchHandler : IDispatchHandler
    {
        public Response response { get ; set; }

        public virtual Task<bool> Execute(JsonElement parameters)
        {
            throw new NotImplementedException();
        }

        public BaseDispatchHandler()
        {
            response = new Response();
        }
    }
}
