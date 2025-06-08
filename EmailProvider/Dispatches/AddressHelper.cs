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

        public static IPAddress GetSMTPIpAddress()
        {
            return IPAddress.Parse(SettingsProvider.GetSMTPServiceIP());
        }

        public static int GetSMTPPublicPort()
        {
            return int.Parse(SettingsProvider.GetSMTPServicePublicPort());
        }

        public static int GetSMTPPrivatePort()
        {
            return int.Parse(SettingsProvider.GetSMTPServicePrivatePort());
        }
    }
}
