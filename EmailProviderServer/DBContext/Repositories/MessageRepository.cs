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
                .Where(m => m.Status == EmailStatuses.EmailStatusNew && m.Direction == EmailDirections.EmailDirectionOut)
                .Include(m => m.MessageRecipients.Where(x=>x.IsOurUser == false))
                .Include(m => m.Files)
                .Take(take)
                .ToListAsync();

            return newMessages;
        }

    }
}
