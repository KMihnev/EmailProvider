//Includes
using EmailServiceIntermediate.Models.Serializables;

namespace EMailProviderClient.Controllers.UserControl
{
    //------------------------------------------------------
    //	UserController
    //------------------------------------------------------

    public class UserController
    {
        public static UserViewModel _currentUser { get; set; }

        public static void SetCurrentUser(UserViewModel user)
        {
            _currentUser = user;
        }

        public static UserViewModel GetCurrentUser()
        {
            return _currentUser;
        }

        public static int GetCurrentUserID()
        {
            return _currentUser.Id;
        }

        public static string GetCurrentUserEmail()
        {
            return _currentUser.Email;
        }

        public static bool IsUserLoggedIn()
        {
            if (_currentUser == null || _currentUser.Id == 0)
                return false;

            return true;
        }
    }
}
