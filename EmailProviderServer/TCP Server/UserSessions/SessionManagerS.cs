using EmailServiceIntermediate.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.TCP_Server.UserSessions
{
    public static class SessionManagerS
    {
        private static readonly ConcurrentDictionary<string, SessionData> sessions = new();

        public static string CreateSession(User user)
        {
            var token = Guid.NewGuid().ToString();
            sessions[token] = new SessionData { User = user, CreatedAt = DateTime.UtcNow };
            return token;
        }

        public static bool TryGetUser(string token, out User user)
        {
            if (sessions.TryGetValue(token, out var session))
            {
                user = session.User;
                return true;
            }
            user = null;
            return false;
        }
    }

    public class SessionData
    {
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
