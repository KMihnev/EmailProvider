using EmailProvider.Dispatches;
using EmailProvider.Logging;
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

                return false;
            }

            SetUpProfileValidationS setUpProfileValidator = new SetUpProfileValidationS();
            setUpProfileValidator.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypeName, user.Name);
            setUpProfileValidator.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypePhoneNumber, user.PhoneNumber);
            setUpProfileValidator.AddValidation(EmailProvider.Enums.UserValidationTypes.ValidationTypeCountry, user.CountryId.ToString());

            if (!setUpProfileValidator.Validate())
            {

                return false;
            }

            var userExists = _userService.GetById(user.Id) != null;
            if (!userExists)
            {

                return false;
            }

            try
            {
                await _userService.UpdateAsync(user.Id, user);
               OutPackage.Serialize(user);
            }
            catch (Exception ex)
            {

                return false;
            }

            return true;
        }
    }
}
