using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailServiceIntermediate.Models;
using EmailProviderServer.Validation.User;
using EmailProviderServer.DBContext.Services.Base;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class RegisterDispatch : BaseDispatchHandler
    {
        private readonly IUserService _userService;

        public RegisterDispatch(IUserService userService)
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
            registerValidationS.AddValidation(EmailServiceIntermediate.Enums.UserValidationTypes.ValidationTypeEmail, user.Email);
            registerValidationS.AddValidation(EmailServiceIntermediate.Enums.UserValidationTypes.ValidationTypePassword, user.Password);

            if (!registerValidationS.Validate())
            {
                errorMessage = LogMessages.InvalidData;
                return false;
            }

            if (await _userService.CheckIfExistsEmailAsync(user.Email))
            {
                errorMessage = LogMessages.UserAlreadyExists;
                return false;
            }

            try
            {
                UserSerializable userSerializable = await _userService.CreateAsync<UserSerializable>(user);
                OutPackage.Serialize(true);
                OutPackage.Serialize(userSerializable);
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
