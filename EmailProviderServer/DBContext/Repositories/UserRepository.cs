using EmailProviderServer.DBContext.Repositories.Base;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories
{
    public class UserRepository : RepositoryS<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base (context)
        {

        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetUserByName(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> CheckIfExistsEmail(string email)
        {
            var user = await GetUserByEmail(email);
            return user != null;
        }
    }
}
