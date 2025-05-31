using EmailProviderServer.TCP_Server.Dispatches;
using EmailProviderServer.TCP_Server.UserSessions;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.TCP_Server.Dispatches.Interfaces;

public abstract class BaseDispatchHandler
{
    protected virtual bool RequiresSession => true;

    protected User SessionUser { get; private set; }
    public string errorMessage { get; protected set; }

    public abstract Task<bool> Execute(SmartStreamArray inPackage, SmartStreamArray outPackage);

    public static async Task<bool> HandleRequestAsync(
        SmartStreamArray inPackage,
        SmartStreamArray outPackage,
        DispatchMapper dispatchMapper)
    {
        try
        {
            inPackage.Deserialize(out string token);

            inPackage.Deserialize(out int dispatchCode);
            BaseDispatchHandler dispatchHandler = dispatchMapper.MapDispatch(dispatchCode);
            if (dispatchHandler == null)
            {
                outPackage.Serialize(false);
                outPackage.Serialize("Unknown dispatch.");
                return false;
            }

            if (dispatchHandler.RequiresSession)
            {
                if (!SessionManagerS.TryGetUser(token, out var user))
                {
                    outPackage.Serialize(false);
                    outPackage.Serialize("Invalid session.");
                    return false;
                }
                dispatchHandler.SessionUser = user;
            }

            if(!await dispatchHandler.Execute(inPackage, outPackage))
            {
                SetResponseFailed(outPackage, "Execution failed.");
                return false;
            }
        }
        catch (Exception ex)
        {
            SetResponseFailed(outPackage, "Internal error.");
            return false;
        }

        return true;
    }

    private static void SetResponseFailed(SmartStreamArray ResponsePackage, string error = "")
    {
        ResponsePackage.Serialize(false);
        ResponsePackage.Serialize(error);
    }
}
