using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailProviderClient.Controllers.UserControl
{
    public static class SessionControllerC
    {
        public static string Token { get; private set; }

        public static void Set(string token) => Token = token;

        public static void Clear() => Token = null;
    }
}
