//Includes

namespace EmailServiceIntermediate.Enums
{
    //------------------------------------------------------
    //	LogType
    //------------------------------------------------------
    public enum LogType
    {
        LogTypeNone = 0,
        LogTypeScreen,
        LogTypeLog,
        LogTypeScreenLog,
    }

    //------------------------------------------------------
    //	LogSeverity
    //------------------------------------------------------
    public enum LogSeverity
    {
        Info = 0,
        Warning,
        Error,
    }
}
