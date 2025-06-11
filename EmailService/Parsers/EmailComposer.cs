using EmailProvider.Models.Serializables;
using EmailServiceIntermediate.Models.Serializables;
using System.Text;

namespace EmailService.Parsers
{
    public static class EmailComposer
    {
        public static string Compose(EmailViewModel email)
        {
            var builder = new StringBuilder();

            var toHeader = string.Join(", ", email.Recipients.Select(r => r.Email));

            var boundary = "boundary-" + Guid.NewGuid().ToString("N");

            builder.AppendLine($"From: <{email.FromEmail}>");
            builder.AppendLine($"To: {toHeader}");
            builder.AppendLine($"Subject: {email.Subject}");
            builder.AppendLine($"Message-ID: <{Guid.NewGuid()}@{email.FromEmail.Split('@')[1]}>");
            builder.AppendLine($"Date: {DateTime.UtcNow:R}");
            builder.AppendLine("MIME-Version: 1.0");
            builder.AppendLine($"Content-Type: multipart/alternative; boundary=\"{boundary}\"");
            builder.AppendLine();

            builder.AppendLine($"--{boundary}");
            builder.AppendLine("Content-Type: text/plain; charset=\"utf-8\"");
            builder.AppendLine("Content-Transfer-Encoding: 7bit");
            builder.AppendLine();
            builder.AppendLine(StripHtml(email.Body));
            builder.AppendLine();

            builder.AppendLine($"--{boundary}");
            builder.AppendLine("Content-Type: text/html; charset=\"utf-8\"");
            builder.AppendLine("Content-Transfer-Encoding: 7bit");
            builder.AppendLine();
            builder.AppendLine(email.Body);
            builder.AppendLine();

            builder.Append($"--{boundary}--");

            return builder.ToString()
                          .Replace("\r\n", "\n")
                          .Replace("\r", "")
                          .Replace("\n", "\r\n");
        }

        private static string StripHtml(string html)
        {
            return System.Text.RegularExpressions.Regex
                .Replace(html, "<.*?>", string.Empty)
                .Replace("&nbsp;", " ")
                .Replace("&amp;", "&");
        }
    }
}
