//Includes

namespace EmailServiceIntermediate.Settings
{
    //------------------------------------------------------
    //	SettingsProvider
    //------------------------------------------------------


    /// <summary> Клас за предоставяне на специфични настройки </summary>
    public class SettingsProvider
     {
         public static string GetServerPort()
         {
             return GlSettingsIni.IniFile.Read(SettingsNames._paramServerPort, SectionNames._sectionShared);
         }

         public static string GetServerIP()
         {
             return GlSettingsIni.IniFile.Read(SettingsNames._paramServerIP, SectionNames._sectionShared);
         }

        public static string GetLogPath()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramLogFilePath, SectionNames._sectionShared);
        }

    }

}
