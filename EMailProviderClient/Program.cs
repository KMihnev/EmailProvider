using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Lang;
using EMailProviderClient.LangSupport;
using EMailProviderClient.Views.User;

namespace EMailProviderClient
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();

            Application.ApplicationExit += (_, _) => Cleanup();

            using var startUp = new StartUp();
            var result = startUp.ShowDialog();

            if (result == DialogResult.OK)
            {
                var user = UserController.GetCurrentUser();
                if (user != null)
                {
                    var translations = LangSupportDispatchesC.LoadTranslations(user.PrefferedLanguageId).Result;
                    if (translations != null)
                        DlgLangSupport.Load(translations);
                }

                Application.Run(new Container());
            }
            else
            {
                Application.Exit(); 
            }
        }

        private static void Cleanup()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
