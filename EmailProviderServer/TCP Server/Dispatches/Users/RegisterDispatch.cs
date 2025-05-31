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
    //	RegisterDispatch
    //------------------------------------------------------
    public class RegisterDispatch : BaseDispatchHandler
    {
        protected override bool RequiresSession => false;

        private readonly IUserService _userService;

        //Constructor
        public RegisterDispatch(IUserService userService)
        {
            _userService = userService;
        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            User user = new User();
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
                UserViewModel userSerializable = await _userService.CreateAsync<UserViewModel>(user);

                User newUser = await _userService.GetByIdAsync<User>(userSerializable.Id);
                var token = SessionManagerS.CreateSession(newUser);

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
