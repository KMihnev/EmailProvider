//Includes
using EmailServiceIntermediate.Settings;


namespace EMailProviderClient.Settings
{
    //------------------------------------------------------
    //	SettingsProviderC
    //------------------------------------------------------
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
