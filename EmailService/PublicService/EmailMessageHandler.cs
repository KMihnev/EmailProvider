using EmailProvider.Enums;
using EmailProvider.Models.Serializables;
using EmailService.Parsers;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Models.Serializables;
using System.Text.RegularExpressions;

namespace EmailService.PublicService
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

            string selectedBody = string.Empty;

            if (message.Parts.Count > 0)
            {
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
            }
            else
            {
                selectedBody = message.Body;
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
                Subject = subject,
                Body = selectedBody,
                Direction = EmailDirections.EmailDirectionIn,
                Status = EmailStatuses.EmailStatusComplete
            };

            BulkIncomingMessageSerializable rawMessage = new BulkIncomingMessageSerializable();
            rawMessage.RawData = body;

            var request = new SmartStreamArray();
            var response = new SmartStreamArray();
            request.Serialize(DispatchEnums.ReceiveEmails);
            request.Serialize(emailModel);
            request.Serialize(rawMessage);

            var dispatcher = new DispatchHandlerService();
            await dispatcher.Execute(request, response);
        }
    }

}
