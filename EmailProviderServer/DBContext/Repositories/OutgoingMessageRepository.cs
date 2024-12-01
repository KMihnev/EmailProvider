using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories
{
    public class OutgoingMessageRepository : IOutgoingMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public OutgoingMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(OutgoingMessage entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddInnerMessageAsync(OutgoingMessage innerMessage)
        {
            await _context.OutgoingMessages.AddAsync(innerMessage);
            await _context.SaveChangesAsync();
        }

        public Task AddOutgoingMessageAsync(OutgoingMessage outgoingMessage)
        {
            throw new NotImplementedException();
        }

        public IQueryable<OutgoingMessage> All()
        {
            throw new NotImplementedException();
        }

        public IQueryable<OutgoingMessage> AllAsNoTracking()
        {
            throw new NotImplementedException();
        }

        public void Delete(OutgoingMessage entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<OutgoingMessage> GetByID(int nId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(OutgoingMessage entity)
        {
            throw new NotImplementedException();
        }
    }
}
