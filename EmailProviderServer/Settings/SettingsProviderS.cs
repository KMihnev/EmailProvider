using EmailServiceIntermediate.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.Settings
{
    public class SettingsProviderS
    {
        public static string GetServerName()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramServerName, SectionNames._sectionServer);
        }

        public static string GetDatabaseName()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramDatabaseName, SectionNames._sectionServer);
        }

    }
}
