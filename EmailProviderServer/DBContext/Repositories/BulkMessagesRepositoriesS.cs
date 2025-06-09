using EmailProviderServer.DBContext.Repositories.Base;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Repositories
{
    public class BulkIncomingMessagesRepository : RepositoryS<BulkIncomingMessage>, IBulkIncomingMessagesRepositoryS
    {
        public BulkIncomingMessagesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddBulkIncomingMessageAsync(BulkIncomingMessage bulkMessage)
        {
            await _context.BulkIncomingMessages.AddAsync(bulkMessage);
            await _context.SaveChangesAsync();
        }
    }

    public class BulkOutgoingMessagesRepository : RepositoryS<BulkOutgoingMessage>, IBulkOutgoingMessagesRepositoryS
    {
        public BulkOutgoingMessagesRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task AddBulkOutgoingMessageAsync(BulkOutgoingMessage bulkMessage)
        {
            await _context.BulkOutgoingMessages.AddAsync(bulkMessage);
            await _context.SaveChangesAsync();
        }
    }
}
