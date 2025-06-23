//Includes

namespace EmailServiceIntermediate.Enums
{
    //------------------------------------------------------
    //	DispatchEnums
    //------------------------------------------------------

    /// <summary> Кодове за идентифициране на RPC Request-ите </summary>
    public enum DispatchEnums : short
    {
        //Client - Server
        Empty = 0,
        Login,
        Register,
        SetUpProfile,
        GetCountries,
        SendEmail,
        GetEmail,
        DeleteEmails,
        ReceiveEmails,
        LoadIncomingEmails,
        LoadOutgoingEmails,
        LoadDrafts,
        LoadEmailsByFolder,
        LoadFolders,
        AddFolder,
        DeleteFolder,
        MarkEmailsAsRead,
        MarkEmailsAsUnread,
        LoadDeletedEmails,
        MoveEmailsToFolder,
        RemoveEmailsFromFolder,
        EditProfile,
        LoadStatistics,
        MakeAnnouncement,

        //Server - Service
        SendEmailToService,
    }
}
