using EmailProvider.Logging;
using EmailProviderServer.DBContext;
using EmailProviderServer.DBContext.Repositories;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailServiceIntermediate.Models;
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
            User user;

            try
            {
                if (!parameters.TryGetProperty("user", out JsonElement userElement))
                {
                    return false;
                }

                user = JsonSerializer.Deserialize<User>(userElement.GetRawText());

                if (user == null)
                {
                    Logger.Log(LogMessages.InvalidUserDetails, EmailProvider.Enums.LogType.LogTypeLog, EmailProvider.Enums.LogSeverity.Error);
                    return false;
                }
            }
            catch (JsonException)
            {
                Logger.Log(LogMessages.InvalidUserDetails, EmailProvider.Enums.LogType.LogTypeLog, EmailProvider.Enums.LogSeverity.Error);
                return false;
            }

            RegisterValidationS registerValidationS = new RegisterValidationS();
            registerValidationS.AddValidation(EmailProvider.Enums.ValidationTypes.ValidationTypeEmail, user.Email);
            registerValidationS.AddValidation(EmailProvider.Enums.ValidationTypes.ValidationTypePassword, user.Password);
            if (!registerValidationS.Validate())
                return false;

            var userExists = _userService.GetByEmail(user.Email) != null;
            if (userExists)
            {
                Logger.Log(LogMessages.UserAlreadyExists, EmailProvider.Enums.LogType.LogTypeLog, EmailProvider.Enums.LogSeverity.Error);
                return false;
            }

            try
            {
                await _userService.CreateAsync(user);
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
