//Includes
using EMailProviderClient.Controllers.UserControl;

namespace EMailProviderClient.Controllers.Email
{
    //------------------------------------------------------
    //	DashboardController
    //------------------------------------------------------
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
