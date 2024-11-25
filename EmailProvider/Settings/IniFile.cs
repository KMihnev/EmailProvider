using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace EmailProvider.Settings
{
    public class IniFile
    {
        string Path;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);


        public IniFile(string IniPath)
        {
            Path = new FileInfo(IniPath).FullName;
        }

        public string Read(string Key, string Section)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }
    }

    public static class GlSettingsIni
    {
        static string IniDirectory = "E:\\EmailDomain" + "\\Settings.ini";

        private static IniFile _iniFile;

        public static IniFile IniFile
        {
            get
            {
                if (_iniFile == null)
                {
                    _iniFile = new IniFile(IniDirectory);
                }

                return _iniFile;
            }
            set
            {
                _iniFile = value;
            }
        }
    }
}
