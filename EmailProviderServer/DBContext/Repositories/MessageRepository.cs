// Includes
using EmailProvider.Enums;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmailProviderServer.DBContext.Repositories
{
    //------------------------------------------------------
    //	MessageRepository
    //------------------------------------------------------
    public class MessageRepository : RepositoryS<Message>, IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        // Get full message with all related data
        public async Task<Message?> GetByIDIncludingAll(int id)
        {
            return await _dbSet
                .Include(m => m.MessageRecipients)
                .Include(m => m.Files)
                .Include(m => m.UserMessages)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // Get multiple messages by IDs, including related data
        public async Task<List<Message>> GetByIDsIncludingAll(IEnumerable<int> ids)
        {
            return await _dbSet
                .Where(m => ids.Contains(m.Id))
                .Include(m => m.MessageRecipients)
                .Include(m => m.Files)
                .Include(m => m.UserMessages)
                .ToListAsync();
        }

        public async Task<List<Message>> GetMessagesForSend(int take)
        {
            var newMessages = await _dbSet
                .Where(m => m.Status == EmailStatuses.EmailStatusNew &&
                            m.Direction == EmailDirections.EmailDirectionOut)
                .Include(m => m.MessageRecipients)
                .Include(m => m.Files)
                .Include(m => m.UserMessages)
                .Take(take)
                .ToListAsync();

            var result = new List<Message>();

            foreach (var message in newMessages)
            {
                foreach (var recipient in message.MessageRecipients)
                {
                    var messageCopy = new Message
                    {
                        Id = message.Id,
                        Subject = message.Subject,
                        Body = message.Body,
                        Status = message.Status,
                        Direction = message.Direction,
                        DateOfRegistration = message.DateOfRegistration,
                        Files = message.Files?.ToList(), // Shallow copy is fine if you won't mutate
                        UserMessages = message.UserMessages?.ToList(),
                        MessageRecipients = new List<MessageRecipient> { recipient }
                    };

                    result.Add(messageCopy);
                }
            }

            return result;
        }

    }
}
