using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories
{
    public class MessageRepository : IRepositoryS<Message>
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Message> All()
        {
            return _context.Messages;
        }

        public IQueryable<Message> GetByID(int nId)
        {
            return _context.Messages.Where(x => x.Id == nId);
        }

        public IQueryable<Message> AllAsNoTracking()
        {
            return _context.Messages.AsNoTracking();
        }

        public async Task AddAsync(Message entity)
        {
            await _context.Messages.AddAsync(entity);
        }

        public void Update(Message entity)
        {
            _context.Messages.Update(entity);
        }

        public void Delete(Message entity)
        {
            _context.Messages.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
