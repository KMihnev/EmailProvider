using EmailProvider.Enums;
using EmailProvider.Models.Serializables;
using EMailProviderClient.Dispatches.Base;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Models.Serializables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailMessageHandler
    {
        public async Task HandleAsync(string from, List<string> toList, string body)
        {
            Console.WriteLine($"Received email from {from} to:");
            foreach (var to in toList)
                Console.WriteLine($"- {to}");

            Console.WriteLine($"Body:\n{body}");

            var recipients = new List<MessageRecipientSerializable>();
            foreach (var to in toList)
            {
                recipients.Add(new MessageRecipientSerializable { Email = to });
            }

            var message = new EmailViewModel
            {
                FromEmail = from,
                Recipients = recipients,
                Body = body,
                Subject = "SMTP Received",
                Direction = EmailDirections.EmailDirectionIn,
                Status = EmailStatuses.EmailStatusNew
            };

            var request = new SmartStreamArray();
            var response = new SmartStreamArray();
            request.Serialize(DispatchEnums.ReceiveEmails);
            request.Serialize(message);

            var dispatcher = new DispatchHandlerService();
            await dispatcher.Execute(request, response);
        }
    }

}
