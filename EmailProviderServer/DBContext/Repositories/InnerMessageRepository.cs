using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories
{
    public class InnerMessageRepository : IInnerMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public InnerMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(InnerMessage entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddInnerMessageAsync(InnerMessage innerMessage)
        {
            await _context.InnerMessages.AddAsync(innerMessage);
            await _context.SaveChangesAsync();
        }

        public IQueryable<InnerMessage> All()
        {
            throw new NotImplementedException();
        }

        public IQueryable<InnerMessage> AllAsNoTracking()
        {
            throw new NotImplementedException();
        }

        public void Delete(InnerMessage entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<InnerMessage> GetByID(int nId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(InnerMessage entity)
        {
            throw new NotImplementedException();
        }
    }
}
