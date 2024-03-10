using EmailProvider.Logging;
using EmailProviderServer.DBContext;
using EmailProviderServer.DBContext.Repositories;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailProviderServer.Models;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class RegisterHandler : IDispatchHandler
    {
        private readonly UserService _userService;

        public RegisterHandler(UserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Execute(JsonElement parameters)
        {
            if (!parameters.TryGetProperty("Email", out JsonElement emailElement) ||
                !parameters.TryGetProperty("Password", out JsonElement passwordElement))
            {
                return false;
            }

            string email = emailElement.GetString();
            string password = passwordElement.GetString();

            RegisterValidationS registerValidationS = new RegisterValidationS();
            registerValidationS.AddValidation(EmailProvider.Enums.ValidationTypes.ValidationTypeEmail, email);
            registerValidationS.AddValidation(EmailProvider.Enums.ValidationTypes.ValidationTypePassword, password);

            var userExists = _userService.GetByEmail<User>(email) != null;
            if (userExists)
            {
                Logger.Log(LogMessages.UserAlreadyExists, EmailProvider.Enums.LogType.LogTypeLog, EmailProvider.Enums.LogSeverity.Error);
                return false;
            }

            var newUser = new User
            {
                Email = email,
                Password = password
            };

            try
            {
                await _userService.CreateAsync<User>(newUser);
            }
            catch (Exception)
            {
                Logger.Log(LogMessages.ErrorAddingUser, EmailProvider.Enums.LogType.LogTypeLog, EmailProvider.Enums.LogSeverity.Error);
                return false;
            }

            return true;
        }
    }
}
