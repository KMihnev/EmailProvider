using System;
using System.Threading.Tasks;
using EmailService.PublicService;

namespace EmailService.Parsers
{
    public class SmtpCommandProcessor
    {
        private readonly SmtpSession session;

        public SmtpCommandProcessor(SmtpSession session)
        {
            this.session = session;
        }

        public async Task<string> ProcessAsync(string command)
        {
            Console.WriteLine($"C: {command}");

            if (command.StartsWith("STARTTLS", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("S: 220 Ready to start TLS");
                await session.WriteLineAsync("220 Ready to start TLS");
                await session.UpgradeToTlsAsync();
                return "";
            }

            if (command.StartsWith("EHLO", StringComparison.OrdinalIgnoreCase) ||
                command.StartsWith("HELO", StringComparison.OrdinalIgnoreCase))
            {
                string response =
                    "250-tyron.mail\n" +
                    "250-STARTTLS\n" +
                    "250-SIZE 35882577\n" +
                    "250-8BITMIME\n" +
                    "250-ENHANCEDSTATUSCODES\n" +
                    "250 PIPELINING";
                Console.WriteLine($"S: {response.Replace("\n", "\nS: ")}");
                return response;
            }

            if (command.StartsWith("MAIL FROM:", StringComparison.OrdinalIgnoreCase))
            {
                string sender = command[10..].Trim('<', '>');
                session.SetSender(sender);
                Console.WriteLine($"S: 250 OK (MAIL FROM: {sender})");
                return "250 OK";
            }

            if (command.StartsWith("RCPT TO:", StringComparison.OrdinalIgnoreCase))
            {
                string recipient = command[8..].Trim('<', '>');
                session.AddRecipient(recipient);
                Console.WriteLine($"S: 250 OK (RCPT TO: {recipient})");
                return "250 OK";
            }

            if (command.StartsWith("DATA", StringComparison.OrdinalIgnoreCase))
            {
                session.BeginData();
                Console.WriteLine("S: 354 End data with <CR><LF>.<CR><LF>");
                return "354 End data with <CR><LF>.<CR><LF>";
            }

            if (command.StartsWith("QUIT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("S: 221 Bye");
                return "221 Bye";
            }

            Console.WriteLine("S: 500 Command not recognized");
            return "500 Command not recognized";
        }
    }
}
