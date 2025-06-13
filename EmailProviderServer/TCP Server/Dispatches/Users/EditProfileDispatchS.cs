//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using System;
using System.Threading.Tasks;
using EmailProviderServer.Validation.User;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;
using EmailProvider.Models.Serializables;
using EmailProviderServer.Helpers;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    //------------------------------------------------------
    //	EditProfileDispatchS
    //------------------------------------------------------
    public class EditProfileDispatchS : BaseDispatchHandler
    {
        private readonly IUserService _userService;

        //Constructor
        public EditProfileDispatchS(IUserService userService)
        {
            _userService = userService;
        }

        //Methods
        public override async Task<bool> Execute(SmartStreamArray InPackage, SmartStreamArray OutPackage)
        {
            UserViewModel user;
            ChangePasswordModel changePasswordModel = null;
            try
            {
                InPackage.Deserialize(out user);
                InPackage.Deserialize(out changePasswordModel);

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
            if(!string.IsNullOrEmpty(user.Name))
                setUpProfileValidator.AddValidation(EmailServiceIntermediate.Enums.UserValidationTypes.ValidationTypeName, user.Name);
            if (!string.IsNullOrEmpty(user.PhoneNumber))
                setUpProfileValidator.AddValidation(EmailServiceIntermediate.Enums.UserValidationTypes.ValidationTypePhoneNumber, user.PhoneNumber);

            setUpProfileValidator.AddValidation(EmailServiceIntermediate.Enums.UserValidationTypes.ValidationTypeCountry, user.CountryId.ToString());
            if(changePasswordModel != null)
            {
                setUpProfileValidator.AddValidation(EmailServiceIntermediate.Enums.UserValidationTypes.ValidationTypePassword, changePasswordModel.NewPassword);
            }

            if (!setUpProfileValidator.Validate())
            {
                errorMessage = LogMessages.InvalidData;
                return false;
            }

            User CurrentUser = await _userService.GetByIdAsync<User>(user.Id);

            if (CurrentUser == null)
            {
                errorMessage = LogMessages.UserNotFound;
                return false;
            }

            CurrentUser.PhoneNumber = user.PhoneNumber;
            CurrentUser.CountryId = user.CountryId;
            CurrentUser.Name = user.Name;
            CurrentUser.Photo = user.Photo;

            bool bUpdatePassword = false;
            if(changePasswordModel != null )
            {
                if(CurrentUser.Password != EncryptionHelper.HashPassword(changePasswordModel.OldPassword))
                {
                    errorMessage = "Incorrect Password!";
                    return false;
                }

                if (changePasswordModel.NewPassword != changePasswordModel.ConfirmPassword)
                {
                    errorMessage = "Passwords dont match!";
                    return false;
                }

                bUpdatePassword = true;
                CurrentUser.Password = changePasswordModel.NewPassword;
            }

            try
            {
                UserViewModel userSerializable = await _userService.UpdateAsync<UserViewModel>(CurrentUser, bUpdatePassword);
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
