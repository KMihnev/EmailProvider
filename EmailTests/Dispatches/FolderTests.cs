using Xunit;
using System.Threading.Tasks;
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Dispatches.Users;
using EMailProviderClient.Dispatches.Folders;
using EMailProviderClient.Controllers.UserControl;
using System;
using EmailProvider.Models.Serializables;
using EmailServiceIntermediate.Models;
using System.Configuration;

namespace EmailTests.Dispatches
{
    public class FolderDispatchesC_IntegrationTests
    {
        [Fact]
        public async Task RegisterAndAddFolder_ShouldSucceed_UsingInMemoryDb()
        {
            Environment.SetEnvironmentVariable("USE_INMEMORY_DB", "true");

            var userModel = new User
            {
                Password = "securE123",
                Email = $"dispatch@{EmailServiceIntermediate.Settings.SettingsProvider.GetEmailDomain()}"
            };

            var registerResult = await UserDispatchesC.Register(userModel);
            Assert.True(registerResult);

            var folderModel = new FolderViewModel
            {
                Name = "TestFolderFromDispatch",
            };

            var folderResult = await AddFolderDispatchC.AddFolder(folderModel);
            Assert.True(folderResult != null);
        }
    }
}
