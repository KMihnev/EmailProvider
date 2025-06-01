//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.Validation.User;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;
using EmailProviderServer.TCP_Server.UserSessions;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    //------------------------------------------------------
    //	LoginDispatch
    //------------------------------------------------------
    public class LoginDispatch : BaseDispatchHandler
    {
        protected override bool RequiresSession => false;

        private readonly IUserService _userService;

        //Constructor
        public LoginDispatch(IUserService userService)
        {
            _userService = userService;
        }

        //Methods
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
                logInValidator.AddValidation(EmailServiceIntermediate.Enums.UserValidationTypes.ValidationTypeEmail, user.Email);
                bIsUsingEmail = true;
            }
            else
                logInValidator.AddValidation(EmailServiceIntermediate.Enums.UserValidationTypes.ValidationTypeEmail, user.Name);

            logInValidator.AddValidation(EmailServiceIntermediate.Enums.UserValidationTypes.ValidationTypePassword, user.Password);

            if (!logInValidator.Validate())
            {
                errorMessage = LogMessages.InvalidData;
                return false;
            }

            User recUser;
            if (bIsUsingEmail)
                recUser = await _userService.GetByEmailAsync<User>(user.Email);
            else
                recUser = await _userService.GetByNameAsync<User>(user.Name);

            if(bIsUsingEmail && recUser == null)
            {
                errorMessage = LogMessages.NoUserForEmail;
                return false;
            }

            if (!bIsUsingEmail && recUser == null)
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
                UserViewModel userSerializable = await _userService.GetByIdAsync<UserViewModel>(recUser.Id);

                var token = SessionManagerS.CreateSession(recUser);

                OutPackage.Serialize(true);
                OutPackage.Serialize(token);
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
