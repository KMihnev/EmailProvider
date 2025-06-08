using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class SmtpCommandProcessor
    {
        private readonly SmtpSession session;

        public SmtpCommandProcessor(SmtpSession session)
        {
            this.session = session;
        }

        public Task<string> ProcessAsync(string command)
        {
            if (command.StartsWith("STARTTLS"))
            {
                session.MarkStartTlsRequested();
                return Task.FromResult("220 Ready to start TLS");
            }

            if (command.StartsWith("EHLO") || command.StartsWith("HELO"))
                return Task.FromResult("250-tyron.mail\n250-STARTTLS\n250-SIZE 35882577\n250-8BITMIME\n250-ENHANCEDSTATUSCODES\n250 PIPELINING");

            if (command.StartsWith("MAIL FROM:"))
            {
                session.SetSender(command[10..].Trim('<', '>'));
                return Task.FromResult("250 OK");
            }

            if (command.StartsWith("RCPT TO:"))
            {
                session.AddRecipient(command[8..].Trim('<', '>'));
                return Task.FromResult("250 OK");
            }

            if (command.StartsWith("DATA"))
            {
                session.BeginData();
                return Task.FromResult("354 End data with <CR><LF>.<CR><LF>");
            }

            if (command.StartsWith("QUIT"))
                return Task.FromResult("221 Bye");

            return Task.FromResult("500 Command not recognized");
        }
    }

}
