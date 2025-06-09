using EmailProvider.Models.Serializables;
using EmailServiceIntermediate.Models.Serializables;
using System;
using System.Text;

namespace EmailService.Parsers
{
    public static class EmailComposer
    {
        public static string Compose(EmailViewModel email)
        {
            var builder = new StringBuilder();

            var toHeader = string.Join(", ", email.Recipients.Select(r => r.Email));

            builder.AppendLine($"From: {email.FromEmail}");
            builder.AppendLine($"To: {toHeader}");
            builder.AppendLine($"Subject: {email.Subject}");
            builder.AppendLine($"Date: {DateTime.UtcNow:R}");
            builder.AppendLine("MIME-Version: 1.0");
            builder.AppendLine("Content-Type: text/plain; charset=\"utf-8\"");
            builder.AppendLine("Content-Transfer-Encoding: 7bit");
            builder.AppendLine();
            builder.AppendLine(email.Body);

            return builder.ToString();
        }
    }
}
