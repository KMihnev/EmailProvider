using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailProviderClient.LangSupport
{
    public static class DlgLangSupport
    {
       private static Dictionary<string, string> _translations = new();

       public static void Load(Dictionary<string, string> translations)
       {
           _translations = translations;
       }

       public static string Get(string key)
       {
           return _translations.TryGetValue(key, out var value) ? value : $"[{key}]";
       }

       public static string Statistics => Get(nameof(Statistics));
       public static string MyAccount => Get(nameof(MyAccount));
       public static string UserAccount => Get(nameof(UserAccount));
       public static string Container => Get(nameof(Container));
       public static string ExitConfirmation => Get(nameof(ExitConfirmation));
       public static string ConfirmExit => Get(nameof(ConfirmExit));
       public static string WriteNew => Get(nameof(WriteNew));
       public static string Filter => Get(nameof(Filter));
       public static string Date => Get(nameof(Date));
       public static string Subject => Get(nameof(Subject));
       public static string Content => Get(nameof(Content));
       public static string SortByDate => Get(nameof(SortByDate));
       public static string FromTo => Get(nameof(FromTo));
       public static string ClearFilter => Get(nameof(ClearFilter));
       public static string MakeAnnouncement => Get(nameof(MakeAnnouncement));
       public static string SenderEmail => Get(nameof(SenderEmail));
       public static string ReceiverEmail => Get(nameof(ReceiverEmail));
       public static string MoveToBin => Get(nameof(MoveToBin));
       public static string Refresh => Get(nameof(Refresh));
       public static string RemoveFromFolder => Get(nameof(RemoveFromFolder));
       public static string ActionAvailableOnCustomFolders => Get(nameof(ActionAvailableOnCustomFolders));
       public static string InvalidOperation => Get(nameof(InvalidOperation));
       public static string FailedToRemoveEmail => Get(nameof(FailedToRemoveEmail));
       public static string Error => Get(nameof(Error));
       public static string MoveToFolder => Get(nameof(MoveToFolder));
       public static string FailedToMoveToFolder => Get(nameof(FailedToMoveToFolder));
       public static string MarkAsRead => Get(nameof(MarkAsRead));
       public static string MarkAsUnread => Get(nameof(MarkAsUnread));
       public static string DraftEmail => Get(nameof(DraftEmail));
       public static string SentEmail => Get(nameof(SentEmail));
       public static string Sender => Get(nameof(Sender));
       public static string ReceivedEmail => Get(nameof(ReceivedEmail));
       public static string Send => Get(nameof(Send));
       public static string Close => Get(nameof(Close));
       public static string NewEmail => Get(nameof(NewEmail));
       public static string Uplaod => Get(nameof(Uplaod));
       public static string Download => Get(nameof(Download));
       public static string Remove => Get(nameof(Remove));
       public static string Email => Get(nameof(Email));
       public static string ByDate => Get(nameof(ByDate));
       public static string EndDate => Get(nameof(EndDate));
       public static string BeginDate => Get(nameof(BeginDate));
       public static string ByEmail => Get(nameof(ByEmail));
       public static string Apply => Get(nameof(Apply));
       public static string Clear => Get(nameof(Clear));
       public static string FilterEmails => Get(nameof(FilterEmails));
       public static string ExtendedFilters => Get(nameof(ExtendedFilters));
       public static string FileDownloadSuccessfully => Get(nameof(FileDownloadSuccessfully));
       public static string DownloadFile => Get(nameof(DownloadFile));
       public static string Folders => Get(nameof(Folders));
       public static string Deleted => Get(nameof(Deleted));
       public static string Incoming => Get(nameof(Incoming));
       public static string Outgoing => Get(nameof(Outgoing));
       public static string NewFolderEditing => Get(nameof(NewFolderEditing));
       public static string FailedToAddFolder => Get(nameof(FailedToAddFolder));
       public static string Add => Get(nameof(Add));
       public static string Delete => Get(nameof(Delete));
       public static string ChangePassword => Get(nameof(ChangePassword));
       public static string OldPassWord => Get(nameof(OldPassWord));
       public static string NewPassword => Get(nameof(NewPassword));
       public static string RepeatPassword => Get(nameof(RepeatPassword));
       public static string OK => Get(nameof(OK));
       public static string LogIn => Get(nameof(LogIn));
       public static string EmailUsername => Get(nameof(EmailUsername));
       public static string Password => Get(nameof(Password));
       public static string DontHaveAccount => Get(nameof(DontHaveAccount));
       public static string Register => Get(nameof(Register));
       public static string Name => Get(nameof(Name));
       public static string SeyUpProfile => Get(nameof(SeyUpProfile));
       public static string ProfilePicture => Get(nameof(ProfilePicture));
       public static string Continue => Get(nameof(Continue));
       public static string Welcome => Get(nameof(Welcome));
       public static string NumberOfUsers => Get(nameof(NumberOfUsers));
       public static string NumberOfOutgoingEmail => Get(nameof(NumberOfOutgoingEmail));
       public static string NumberOfIncomingEmails => Get(nameof(NumberOfIncomingEmails));
       public static string Save => Get(nameof(Save));
       public static string Edit => Get(nameof(Edit));
       public static string FailedToSaveChanges => Get(nameof(FailedToSaveChanges));
       public static string ProfileUpdatedSuccessfully => Get(nameof(ProfileUpdatedSuccessfully));
       public static string Saved => Get(nameof(Saved));
       public static string LogOut => Get(nameof(LogOut));
       public static string EmailPost => Get(nameof(EmailPost));
       public static string NamePost => Get(nameof(NamePost));
       public static string CountryPost => Get(nameof(CountryPost));
       public static string PhoneNumberPost => Get(nameof(PhoneNumberPost));

       public static string ChangeLanguageQuestion => Get(nameof(ChangeLanguageQuestion));
       public static string ChangeLanguage => Get(nameof(ChangeLanguage));

       public static string Receiver => Get(nameof(Receiver));

        public static string Files => Get(nameof(Files));

        public static string RestartNeeded => Get(nameof(RestartNeeded));
    }
}
