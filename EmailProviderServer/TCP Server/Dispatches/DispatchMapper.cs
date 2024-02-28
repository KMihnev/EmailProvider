//Includes

using EmailProvider.Enums;
using System.Text.Json;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class MethodRequest
    {
        public DispatchEnums eDispatch { get; set; }
        public JsonElement Parameters { get; set; }
    }

    public static class DispatchMapper
    {
        public static void MapDispatch(MethodRequest request)
        {
            switch (request.eDispatch)
            {
                case DispatchEnums.Login:
                    LogInHandler.Login(request.Parameters);
                    break;
            }
        }
    }
}
