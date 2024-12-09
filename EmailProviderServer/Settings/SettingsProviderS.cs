//Includes
using EmailServiceIntermediate.Settings;

namespace EmailProviderServer.Settings
{
    //------------------------------------------------------
    //	SettingsProviderS
    //------------------------------------------------------
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
