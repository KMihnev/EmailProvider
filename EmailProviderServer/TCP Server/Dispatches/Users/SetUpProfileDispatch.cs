using EmailProvider.Logging;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.Validation;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class SetUpProfileDispatch : BaseDispatchHandler
    {
        private readonly UserService _userService;

        public SetUpProfileDispatch(UserService userService)
        {
            _userService = userService;
            response = new EmailProvider.Reponse.Response();
        }

        public async override Task<bool> Execute(JsonElement parameters)
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
                    response.bSuccess = false;
                    response.msgError = LogMessages.InvalidUserDetails;
                    Logger.Log(LogMessages.InvalidUserDetails, EmailProvider.Enums.LogType.LogTypeLog, EmailProvider.Enums.LogSeverity.Error);
                    return false;
                }
            }
            catch (JsonException)
            {
                response.bSuccess = false;
                response.msgError = LogMessages.InvalidUserDetails;
                Logger.Log(LogMessages.InvalidUserDetails, EmailProvider.Enums.LogType.LogTypeLog, EmailProvider.Enums.LogSeverity.Error);
                return false;
            }

            SetUpProfileValidationS setUpProfileValidator = new SetUpProfileValidationS();
            setUpProfileValidator.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypeName, user.Name);
            setUpProfileValidator.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypePhoneNumber, user.PhoneNumber);
            setUpProfileValidator.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypeCountry, user.CountryId.ToString());

            if (!setUpProfileValidator.Validate())
                return false;

            var userExists = _userService.GetById(user.Id) != null;
            if (!userExists)
            {
                response.bSuccess = false;
                response.msgError = LogMessages.DispatchErrorUserNotFound;
                Logger.Log(LogMessages.DispatchErrorUserNotFound, EmailProvider.Enums.LogType.LogTypeLog, EmailProvider.Enums.LogSeverity.Error);
                return false;
            }

            try
            {
                await _userService.UpdateAsync(user.Id, user);
                response.Data = user;
            }
            catch (Exception)
            {
                response.bSuccess = false;
                response.msgError = LogMessages.DispatchErrorSetUpProfile;
                Logger.Log(LogMessages.DispatchErrorSetUpProfile, EmailProvider.Enums.LogType.LogTypeLog, EmailProvider.Enums.LogSeverity.Error);
                return false;
            }

            return true;
        }
    }
}

