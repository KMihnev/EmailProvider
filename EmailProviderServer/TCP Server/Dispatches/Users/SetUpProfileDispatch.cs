using EmailProvider.Dispatches;
using EmailProvider.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailProviderServer.Validation;
using EmailServiceIntermediate.Models;
using System;
using System.Threading.Tasks;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class SetUpProfileDispatch : BaseDispatchHandler
    {
        private readonly UserService _userService;

        public SetUpProfileDispatch(UserService userService)
        {
            _userService = userService;
        }

        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            UserSerializable user;
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

                return false;
            }

            SetUpProfileValidationS setUpProfileValidator = new SetUpProfileValidationS();
            setUpProfileValidator.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypeName, user.Name);
            setUpProfileValidator.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypePhoneNumber, user.PhoneNumber);
            setUpProfileValidator.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypeCountry, user.CountryId.ToString());

            if (!setUpProfileValidator.Validate())
            {
                errorMessage = LogMessages.InvalidData;
                return false;
            }

            User CurrentUser = _userService.GetById<User>(user.Id);

            if (CurrentUser == null)
            {
                errorMessage = LogMessages.UserNotFound;
                return false;
            }

            CurrentUser.PhoneNumber = user.PhoneNumber;
            CurrentUser.CountryId = user.CountryId;
            CurrentUser.Name = user.Name;

            try
            {
                UserSerializable userSerializable = await _userService.UpdateAsync<UserSerializable>(CurrentUser);
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
