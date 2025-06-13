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

        public static string GetSMTPServiceIP()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramSMTPServiceIP, SectionNames._sectionShared);
        }

        public static string GetSMTPServicePublicPort()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramSMTPPublicServicePort, SectionNames._sectionShared);
        }

        public static string GetSMTPServicePublicCertPFXPath()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramSMTPPublicServiceCertPFXPath, SectionNames._sectionShared);
        }

        public static string GetSMTPServicePublicCertPassword()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramSMTPPublicServiceCertPassword, SectionNames._sectionShared);
        }

        public static string GetSMTPServicePrivatePort()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramSMTPPrivateServicePort, SectionNames._sectionShared);
        }

        public static string GetEmailDomain()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramEmailDomain, SectionNames._sectionShared);
        }

        public static string GetEmailPublicDomain()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramEmailPublicDomain, SectionNames._sectionShared);
        }

        public static string GetDKIMKeyPath()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramDKIMPrivateKeyPath, SectionNames._sectionShared);
        }

        public static string GetDKIMRecordSelector()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramDKIMRecordSelector, SectionNames._sectionShared);
        }

        public static string GetServerCertificateSubject()
        {
            return GlSettingsIni.IniFile.Read(SettingsNames._paramServerCertificateSubject, SectionNames._sectionShared);
        }
    }

}
