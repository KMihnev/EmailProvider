//Includes
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Base;
using EmailProvider.Models.Serializables;

namespace EMailProviderClient.Dispatches.Users
{
    //------------------------------------------------------
    //	UserDispatchesC
    //------------------------------------------------------
    public class UserDispatchesC
    {
        //Methods

        /// <summary> RPC за регистриране </summary>
        public static async Task<bool> Register(EmailServiceIntermediate.Models.User user)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.Register);
                InPackage.Serialize(user);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                    return false;

                string token = "";
                UserViewModel newUser = null;

                OutPackage.Deserialize(out token);
                OutPackage.Deserialize(out newUser);

                if (newUser == null)
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                if (token == null || token.Count() <= 0)
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                SessionControllerC.Set(token);
                UserController.SetCurrentUser(newUser);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogErrorCalling();
                return false;
            }
        }

        /// <summary> RPC за popylwane na данни за профила </summary>
        public static async Task<bool> SetUpProfile(UserViewModel user)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.SetUpProfile);
                InPackage.Serialize(user);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    return false;
                }

                UserViewModel updatedUser = null;
                OutPackage.Deserialize(out updatedUser);

                if (updatedUser == null)
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                UserController.SetCurrentUser(updatedUser);
            }
            catch (Exception ex)
            {
                Logger.LogErrorCalling();
                return false;
            }

            return true;
        }

        /// <summary> RPC за вход </summary>
        public static async Task<bool> LogIn(EmailServiceIntermediate.Models.User user)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.Login);
                InPackage.Serialize(user);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                    return false;

                string token = "";
                UserViewModel newUser = null;

                OutPackage.Deserialize(out token);
                OutPackage.Deserialize(out newUser);

                if (token == null || token.Count() <= 0)
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                if (newUser == null)
                {
                    Logger.LogError(string.Format(LogMessages.NullObject, newUser.GetType().Name));
                    return false;
                }

                SessionControllerC.Set(token);
                UserController.SetCurrentUser(newUser);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogErrorCalling();
                return false;
            }
        }

        /// <summary> RPC за popylwane na данни за профила </summary>
        public static async Task<bool> EditProfile(UserViewModel user, ChangePasswordModel ChangePassword)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.EditProfile);
                InPackage.Serialize(user);
                InPackage.Serialize(ChangePassword);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    return false;
                }

                UserViewModel updatedUser = null;
                OutPackage.Deserialize(out updatedUser);

                if (updatedUser == null)
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                UserController.SetCurrentUser(updatedUser);
            }
            catch (Exception ex)
            {
                Logger.LogErrorCalling();
                return false;
            }

            return true;
        }

        public static async Task<StatisticsViewModel> LoadStatistics()
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.LoadStatistics);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    return null;
                }

                StatisticsViewModel updatedStatistics = null;
                OutPackage.Deserialize(out updatedStatistics);

                if (updatedStatistics == null)
                {
                    Logger.LogErrorCalling();
                    return null;
                }

                return updatedStatistics;
            }
            catch (Exception ex)
            {
                Logger.LogErrorCalling();
                return null;
            }
        }
    }
}
