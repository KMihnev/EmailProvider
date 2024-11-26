using EmailProvider.Dispatches;
using EmailProvider.Logging;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.Validation;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class RegisterHandler : BaseDispatchHandler
    {
        private readonly UserService _userService;

        public RegisterHandler(UserService userService)
        {
            _userService = userService;
        }

        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            User user;
            try
            {
                InPackage.Deserialize(out user);

                if (user == null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.InteralError);
                return false;
            }

            RegisterValidationS registerValidationS = new RegisterValidationS();
            registerValidationS.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypeEmail, user.Email);
            registerValidationS.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypePassword, user.Password);

            if (!registerValidationS.Validate())
            {
                errorMessage = LogMessages.InvalidData;
                return false;
            }

            var userExists = _userService.GetByEmail(user.Email) != null;
            if (userExists)
            {
                errorMessage = LogMessages.UserAlreadyExists;
                return false;
            }

            try
            {
                await _userService.CreateAsync(user);
                OutPackage.Serialize(true);
                OutPackage.Serialize(user);
            }
            catch (Exception ex)
            {
                Logger.LogError(LogMessages.InteralError);
                return false;
            }

            return true;
        }
    }
}
