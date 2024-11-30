using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceIntermediate.Settings
{
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
