using EmailServiceIntermediate.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailProviderClient.Settings
{
    public class SettingsProviderC
    {
        public static string GetServerIP()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramServerName, SectionNames._sectionServer);
        }

        public static string GetServerPort()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramDatabaseName, SectionNames._sectionServer);
        }
    }
}
