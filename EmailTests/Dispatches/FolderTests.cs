using Xunit;
using System.Threading.Tasks;
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Dispatches.Users;
using EMailProviderClient.Dispatches.Folders;
using EMailProviderClient.Controllers.UserControl;
using System;
using System.Collections.Generic;
using EmailProvider.Models.Serializables;
using EmailServiceIntermediate.Models;

namespace EmailTests.Dispatches
{
    public class FolderDispatchesC_IntegrationTests
    {
        private string GenerateTestEmail() =>
            $"test_{Guid.NewGuid():N}@{EmailServiceIntermediate.Settings.SettingsProvider.GetEmailDomain()}";

        private string GenerateTestFolderName(string prefix) =>
            $"{prefix}_{Guid.NewGuid():N}".Substring(0, 30); // Truncate to avoid DB length limits

        [Fact]
        public async Task RegisterAndAddFolder_ShouldSucceed()
        {
            var userModel = new User
            {
                Password = "testA1234",
                Email = GenerateTestEmail()
            };

            Assert.True(await UserDispatchesC.Register(userModel));

            var folderModel = new FolderViewModel
            {
                Name = GenerateTestFolderName("DispatchFolder")
            };

            var folderResult = await AddFolderDispatchC.AddFolder(folderModel);
            Assert.NotNull(folderResult);
        }

        [Fact]
        public async Task RegisterAndGetUserFolders_ShouldReturnCreatedFolder()
        {
            var userModel = new User
            {
                Password = "testA1234",
                Email = GenerateTestEmail()
            };

            Assert.True(await UserDispatchesC.Register(userModel));

            string folderName = GenerateTestFolderName("GetFolder");
            var folderModel = new FolderViewModel { Name = folderName };

            var created = await AddFolderDispatchC.AddFolder(folderModel);
            Assert.NotNull(created);

            List<FolderViewModel> folders = new List<FolderViewModel>();
            bool result = await LoadFoldersDispatchC.LoadFolders(folders);

            Assert.True(result);
            Assert.NotNull(folders);
            Assert.Contains(folders, f => f.Name == folderName);
        }

        [Fact]
        public async Task RegisterAndDeleteFolder_ShouldSucceed()
        {
            var userModel = new User
            {
                Password = "testA1234",
                Email = GenerateTestEmail()
            };

            Assert.True(await UserDispatchesC.Register(userModel));

            var folderModel = new FolderViewModel
            {
                Name = GenerateTestFolderName("ToDelete")
            };

            var created = await AddFolderDispatchC.AddFolder(folderModel);
            Assert.NotNull(created);

            var result = await DeleteFolderDispatchC.DeleteFolder(created.Id);
            Assert.True(result);
        }
    }
}
