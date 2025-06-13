using Xunit;
using System.Threading.Tasks;
using EmailServiceIntermediate.Models;
using EMailProviderClient.Dispatches.Users;
using EmailServiceIntermediate.Settings;
using System;

namespace EmailTests.Dispatches
{
    public class UserDispatchesC_IntegrationTests
    {
        private string GenerateTestEmail() =>
            $"user_{Guid.NewGuid():N}@{SettingsProvider.GetEmailDomain()}";

        private string DefaultPassword => "securePass123";

        [Fact]
        public async Task RegisterUser_ShouldSucceed()
        {
            var user = new User
            {
                Email = GenerateTestEmail(),
                Password = DefaultPassword
            };

            var result = await UserDispatchesC.Register(user);
            Assert.True(result);
        }

        [Fact]
        public async Task RegisterAndLoginUser_ShouldReturnUser()
        {
            var email = GenerateTestEmail();
            var password = DefaultPassword;

            var user = new User
            {
                Email = email,
                Password = password
            };

            Assert.True(await UserDispatchesC.Register(user));

            Assert.True(await UserDispatchesC.LogIn(user));
        }

        [Fact]
        public async Task RegisterAndTryLoginWithWrongPassword_ShouldReturnNull()
        {
            var user = new User
            {
                Email = GenerateTestEmail(),
                Password = DefaultPassword
            };

            Assert.True(await UserDispatchesC.Register(user));
            user.Password = "WorongA123";
            Assert.False(await UserDispatchesC.LogIn(user));
        }

        [Fact]
        public async Task RegisterDuplicateUser_ShouldFail()
        {
            var user = new User
            {
                Email = GenerateTestEmail(),
                Password = DefaultPassword
            };

            Assert.True(await UserDispatchesC.Register(user));
            Assert.False(await UserDispatchesC.Register(user));
        }
    }
}
