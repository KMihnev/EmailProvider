//Includes
using EmailProviderServer.Settings;

namespace EmailProviderServer.DBContext
{
    //------------------------------------------------------
    //	ConnectionStringCreator
    //------------------------------------------------------
    public static class ConnectionStringCreator
    {
        public static string CreateConnectionString()
        {
            string serverName = SettingsProviderS.GetServerName();
            string databaseName = SettingsProviderS.GetDatabaseName();

            if (string.IsNullOrEmpty(serverName) || string.IsNullOrEmpty(databaseName))
            {
                throw new InvalidOperationException("Server name or database name is not specified in the INI file.");
            }

            string connectionString = $"Server={serverName};Database={databaseName};Trusted_Connection=True;TrustServerCertificate=True;";

            return connectionString;
        }
    }
}
