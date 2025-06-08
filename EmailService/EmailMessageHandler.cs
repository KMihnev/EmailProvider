using EmailProvider.Enums;
using EmailProvider.Models.Serializables;
using EMailProviderClient.Dispatches.Base;
using EmailService.Parsers;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Models.Serializables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailMessageHandler
    {
        public async Task HandleAsync(string from, List<string> toList, string body)
        {
            body = Regex.Replace(body, @"(?<=\S)\r\n\r\n(?=\S)", "\r\n");
            var parser = new MimeParser();
            var message = parser.Parse(body);

            message.Headers.TryGetValue("Subject", out var subject);
            message.Headers.TryGetValue("From", out var fromHeader);

            subject ??= "SMTP Received";
            fromHeader ??= from;

            Console.WriteLine($"Parsed email from {fromHeader} to:");
            foreach (var to in toList)
                Console.WriteLine($"- {to}");

            string selectedBody = message.Body;
            foreach (var part in message.Parts)
            {
                if (part.Headers.TryGetValue("Content-Type", out var contentType))
                {
                    if (contentType.StartsWith("text/plain", StringComparison.OrdinalIgnoreCase))
                    {
                        selectedBody = part.Body;
                        break;
                    }
                    else if (contentType.StartsWith("text/html", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(selectedBody))
                    {
                        selectedBody = part.Body;
                    }
                }
            }

            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body:\n{selectedBody}");

            var recipients = toList
                .Select(email => new MessageRecipientSerializable { Email = email })
                .ToList();

            var emailModel = new EmailViewModel
            {
                FromEmail = Regex.Match(fromHeader, @"<([^>]+)>").Groups[1].Value,
                Recipients = recipients,
                Body = selectedBody,
                Subject = subject,
                Direction = EmailDirections.EmailDirectionIn,
                Status = EmailStatuses.EmailStatusComplete
            };

            var request = new SmartStreamArray();
            var response = new SmartStreamArray();
            request.Serialize(DispatchEnums.ReceiveEmails);
            request.Serialize(emailModel);

            var dispatcher = new DispatchHandlerService();
            await dispatcher.Execute(request, response);
        }
    }

}
