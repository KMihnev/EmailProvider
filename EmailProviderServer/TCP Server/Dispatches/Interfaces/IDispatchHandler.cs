using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmailProviderServer.TCP_Server.Dispatches.Interfaces
{
    public interface IDispatchHandler
    {
        abstract Task<bool> Execute(JsonElement parameters);
    }
}
