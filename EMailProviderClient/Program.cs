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
