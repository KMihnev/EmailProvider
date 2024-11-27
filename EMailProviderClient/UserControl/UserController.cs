using EmailProvider.Models.Serializables;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailProviderClient.UserControl
{
    public class UserController
    {
        public static UserSerializable _currentUser { get; set; }

        public static void SetCurrentUser(UserSerializable user)
        {
            _currentUser = user;
        }

        public static UserSerializable GetCurrentUser()
        {
            return _currentUser;
        }

        public static int GetCurrentUserID()
        {
            return _currentUser.Id;
        }

        public static bool IsUserLoggedIn()
        {
            if (_currentUser == null || _currentUser.Id == 0)
                return false;

            return true;
        }
    }
}
