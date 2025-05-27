//Includes
using EmailServiceIntermediate.Settings;
using System.Net;

namespace EmailServiceIntermediate.Dispatches
{
    //------------------------------------------------------
    //	AddressHelper
    //------------------------------------------------------

    /// <summary>
    /// Помощен клас за конструиране на адрес на сървъра
    /// </summary>
    public class AddressHelper
    {
        //Constructor
        public AddressHelper() 
        {

        }

        //Methods
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
