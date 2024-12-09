using EmailProvider.Models.DBModels;
using EmailProviderServer.DBContext.Repositories.Base;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories
{
    public class MessageRepository : RepositoryS<Message>, IMessageRepository
    {

        public MessageRepository(ApplicationDbContext context) : base (context)
        {

        }

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

    }
}
