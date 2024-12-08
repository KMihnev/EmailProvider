using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models.Serializables;
using EMailProviderClient.Controllers.UserControl;
using EMailProviderClient.Dispatches.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailProvider.Models.Serializables;
using EmailProvider.SearchData;
using EmailProvider.Models.DBModels;

namespace EMailProviderClient.Dispatches.Emails
{
    public class LoadEmailsDispatchC
    {
        public static async Task<bool> LoadEmails(List<ViewMessage> outMessageList, SearchData searchData)
        {
            try
            {
                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                //Сериализираме Данните
                InPackage.Serialize((int)DispatchEnums.LoadEmails);
                InPackage.Serialize(searchData);

                //Изпращаме заявката
                DispatchHandlerC dispatchHandlerC = new DispatchHandlerC();

                if (!await dispatchHandlerC.Execute(InPackage, OutPackage))
                {
                    Logger.LogErrorCalling();
                    return false;
                }

                List<ViewMessage> messageList = null;
                OutPackage.Deserialize(out messageList);

                outMessageList = messageList;

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogErrorCalling();
                return false;
            }
        }
    }
}
