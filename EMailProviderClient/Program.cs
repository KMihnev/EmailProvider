using EmailServiceIntermediate.Settings;
using EMailProviderClient.Settings;
using EMailProviderClient.Views.User;

namespace EMailProviderClient
{
    internal static class Program
    {
        static StartUp? _mainForm;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Attach application-wide shutdown
            Application.ApplicationExit += (s, e) =>
            {
                _mainForm?.Shutdown();
            };

            _mainForm = new StartUp();
            Application.Run(_mainForm);
        }
    }
}