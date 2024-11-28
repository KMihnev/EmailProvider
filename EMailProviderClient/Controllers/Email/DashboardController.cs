using EMailProviderClient.Controllers.UserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailProviderClient.Controllers.Email
{
    public class DashboardController
    {
        static EmailProvider _emailProvider;

        private static void Init()
        {
            _emailProvider = new EmailProvider();
        }

        public static void Show()
        {
            if (_emailProvider == null)
                Init();

            if (!UserController.IsUserLoggedIn())
                return;

            _emailProvider.Show();
        }
    }
}
