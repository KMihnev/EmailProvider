using EmailServiceIntermediate.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceIntermediate.Dispatches
{
    public class AddressHelper
    {
        public AddressHelper() { }

        public static IPAddress GetIpAddress()
        {
            return IPAddress.Parse(SettingsProvider.GetServerIP());
        }

        public static int GetPort()
        {
            return int.Parse(SettingsProvider.GetServerPort());
        }
    }
}
