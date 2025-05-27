//Includes
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailProviderServer.DBContext.Repositories
{
    //------------------------------------------------------
    //	UserRepository
    //------------------------------------------------------
    public class UserRepository : RepositoryS<User>, IUserRepository
    {
        //Constructor
        public UserRepository(ApplicationDbContext context) : base (context)
        {

        }

        //Methods
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
