//Includes
using EmailProvider.Models.DBModels;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailProviderServer.DBContext.Repositories
{
    //------------------------------------------------------
    //	MessageRepository
    //------------------------------------------------------
    public class MessageRepository : RepositoryS<Message>, IMessageRepository
    {
        //Constructor
        public MessageRepository(ApplicationDbContext context) : base (context)
        {

        }

        //Methods
        public async Task<List<ViewMessage>> GetCombinedMessagesAsync(int userId, int searchType, string whereClause)
        {
            var results = await _context.ViewMessageSerializable
                .FromSqlInterpolated($"EXEC SP_GET_MESSAGES @UserId = {userId}, @SearchType = {searchType}, @WhereClause = {whereClause}")
                .ToListAsync();

            foreach (var message in results)
            {
                if (!string.IsNullOrEmpty(message.ReceiverEmails))
                {
                    message.ReceiverEmailsList = message.ReceiverEmails.Split(';').ToList();
                }
            }

            return results;
        }

        public async Task<Message> GetByIDIncludingAll(int id)
        {
            return await _dbSet
                .Include(m => m.InnerMessages)
                .ThenInclude(im => im.Sender)
                .Include(m => m.InnerMessages)
                .ThenInclude(im => im.Receiver)
                .Include(m => m.OutgoingMessages)
                .ThenInclude(im => im.Sender)
                .Include(m => m.IncomingMessages)
                .ThenInclude(im => im.Receiver)
                .Include(m => m.Files)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Message>> GetByIDsIncludingAll(IEnumerable<int> ids)
        {
            return await _dbSet
                .Where(m => ids.Contains(m.Id))
                .Include(m => m.InnerMessages)
                .ThenInclude(im => im.Sender)
                .Include(m => m.InnerMessages)
                .ThenInclude(im => im.Receiver)
                .Include(m => m.OutgoingMessages)
                .ThenInclude(om => om.Sender)
                .Include(m => m.IncomingMessages)
                .ThenInclude(im => im.Receiver)
                .Include(m => m.Files)
                .ToListAsync();
        }
    }
}
