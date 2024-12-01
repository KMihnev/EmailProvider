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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> All()
        {
            return _context.Users;
        }

        public IQueryable<User> GetByID(int nId)
        {
            return _context.Users.Where(x=>x.Id == nId);
        }

        public IQueryable<User> AllAsNoTracking()
        {
            return _context.Users.AsNoTracking();
        }

        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
