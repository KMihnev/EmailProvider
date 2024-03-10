using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
namespace EmailProvider.Settings
{
    public class IniFile
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);


        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }

    public static class IniReader
    {
        public static IniFile _glIniFile {  get; private set; }
        public static void InitIni(string path = null)
        {
            _glIniFile = new IniFile(path);
        }

        public static string GetFileLogPath ()
        {
            if ((_glIniFile == default))
                return default;

            return _glIniFile.Read(SettingsNames._glLogFile, SectionNames._glPaths);
        }

        public static string GetServerName()
        {
            if (!IsIniInitialized())
                return default;

            return _glIniFile.Read(SettingsNames._glServerName, SectionNames._glDatabase);
        }

        public static string GetDatabaseName()
        {
            if (!IsIniInitialized())
                return default;

            return _glIniFile.Read(SettingsNames._glDatabaseName, SectionNames._glDatabase);
        }

        private static bool IsIniInitialized()
        {
            if (_glIniFile == default)
                return false;

            return true;
        }

        private static bool IsValueValid(string value)
        {
            if(string.IsNullOrWhiteSpace(value)) 
                return false;

            return true;
        }
    }
}
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
