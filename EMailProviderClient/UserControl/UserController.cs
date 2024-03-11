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
        public static User _currentUser { get; set; }

        public static void SetCurrentUser(User user)
        {
            _currentUser = user;
        }

        public static User GetCurrentUser()
        {
            return _currentUser;
        }
    }
}
