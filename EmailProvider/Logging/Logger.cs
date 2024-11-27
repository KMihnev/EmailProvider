using EmailProvider.Enums;
using EmailProvider.Settings;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace EmailProvider.Logging
{
    public static class Logger
    {
        public static void LogWarning(string message)
        {
            Log(message, LogType.LogTypeScreen, LogSeverity.Warning);
        }

        public static void LogWarning(string message, params object[] args)
        {
            string formattedMessage = string.Format(message, args);
            Log(formattedMessage, LogType.LogTypeScreen, LogSeverity.Warning);
        }

        public static void LogError(string message)
        {
            Log(message, LogType.LogTypeScreenLog, LogSeverity.Error);
        }

        public static void LogErrorCalling([CallerMemberName] string methodName = "")
        {
            LogError(LogMessages.ErrorCalling, methodName);
        }

        public static void LogError(string message, params object[] args)
        {
            string formattedMessage = string.Format(message, args);
            Log(formattedMessage, LogType.LogTypeScreenLog, LogSeverity.Error);
        }


        public static void LogInfo(string message)
        {
            Log(message, LogType.LogTypeScreen, LogSeverity.Info);
        }

        public static void LogInfo(string message, params object[] args)
        {
            string formattedMessage = string.Format(message, args);
            Log(formattedMessage, LogType.LogTypeScreen, LogSeverity.Info);
        }

        public static void Log(string log,LogType eLogType = LogType.LogTypeScreenLog ,LogSeverity eLogSeverity = LogSeverity.Error)
        {
            switch (eLogType)
            {
                case LogType.LogTypeScreen:
                {
                    LogScreen(log, eLogSeverity);
                    break;
                }
                case LogType.LogTypeLog:
                {
                    LogFile(log, eLogSeverity);
                    break;
                }
                case LogType.LogTypeScreenLog:
                {
                    LogScreenLog(log, eLogSeverity);
                    break;
                }
            }

        }

        private  static void LogFile(string log, LogSeverity eLogSeverity)
        {
            string logFilePath = SettingsProvider.GetLogPath();
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath))
                {
                    string logLine = string.Format(LogMessages.FileLogMessage, eLogSeverity.ToString(), log);
                    writer.WriteLine(logLine);
                }
            }
            catch (Exception)
            {
                Console.WriteLine(LogMessages.CantReachLogFileLocation);
            }
        }

        private static void LogScreen(string log, LogSeverity eLogSeverity)
        {
            string logFilePath = SettingsProvider.GetLogPath();
            try
            {
                switch (eLogSeverity)
                {
                    case LogSeverity.Info:
                        {
                            MessageBox.Show(log, LogMessages.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                    case LogSeverity.Warning:
                        {
                            MessageBox.Show(log, LogMessages.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case LogSeverity.Error:
                        {
                            MessageBox.Show(log, LogMessages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine(LogMessages.CantReachLogFileLocation);
            }
        }

        private static void LogScreenLog(string log, LogSeverity eLogSeverity)
        {
            LogScreen(log, eLogSeverity);
            LogFile(log, eLogSeverity);
        }

    }
}
