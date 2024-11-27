using EmailProvider.Dispatches;
using EmailProvider.Logging;
using EmailProvider.Models.Serializables;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.Validation;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class LoginDispatch : BaseDispatchHandler
    {
        private readonly UserService _userService;

        public LoginDispatch(UserService userService)
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

            LogInValidationS logInValidator = new LogInValidationS();

            bool bIsUsingEmail = false;
            if (logInValidator.IsEmail(user.Email))
            {
                logInValidator.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypeEmail, user.Email);
                bIsUsingEmail = true;
            }
            else
                logInValidator.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypeEmail, user.Name);

            logInValidator.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypePassword, user.Password);

            if (!logInValidator.Validate())
            {
                errorMessage = LogMessages.InvalidData;
                return false;
            }

            User recUser;
            if (bIsUsingEmail)
                recUser = _userService.GetByEmail<User>(user.Email);
            else
                recUser = _userService.GetByEmail<User>(user.Name);

            if (recUser == null)
            {
                errorMessage = LogMessages.InteralError;
                return false;
            }


            if(bIsUsingEmail && recUser.Email != user.Email)
            {
                errorMessage = LogMessages.NoUserForEmail;
                return false;
            }

            if (!bIsUsingEmail && recUser.Name != user.Name)
            {
                errorMessage = LogMessages.NoUserForName;
                return false;
            }

            if (recUser.Password != user.Password)
            {
                errorMessage = LogMessages.IncorrectPassword;
                return false;
            }

            try
            {
                UserSerializable userSerializable = _userService.GetById<UserSerializable>(recUser.Id);
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
