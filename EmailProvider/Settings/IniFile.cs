//Includes
using System.Runtime.InteropServices;
using System.Text;

namespace EmailServiceIntermediate.Settings
{
    //------------------------------------------------------
    //	IniFile
    //------------------------------------------------------

    /// <summary> клас за работа с INI филе </summary>
    public class IniFile
    {
        string Path;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        //Constructor
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

    //------------------------------------------------------
    //	GlSettingsIni
    //------------------------------------------------------

    /// <summary> Глобален INI file </summary>
    public static class GlSettingsIni
    {
        /// <summary> Директория на файла TO DO - да се смени да е в starting директорията на приложението </summary>
        static string IniDirectory = Path.Combine(Environment.CurrentDirectory, "Settings.ini");

        /// <summary> същински файл </summary>
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
