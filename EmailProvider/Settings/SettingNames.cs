using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceIntermediate.Settings
{
    public static class SectionNames
    {
        public const string _sectionServer = "SERVER";
        public const string _sectionClient = "CLIENT";
        public const string _sectionShared = "SHARED";
    }
    public static class SettingsNames
    {
        public const string _paramLogFilePath = "FileLogPath";
        public const string _paramServerName = "ServerName";
        public const string _paramDatabaseName = "DataBaseName";
        public const string _paramServerIP = "ServerIP";
        public const string _paramServerPort = "ServerPort";
    }
}
