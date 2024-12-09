//Includes
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailProviderServer.DBContext.Repositories
{
    //------------------------------------------------------
    //	CountryRepository
    //------------------------------------------------------
    public class CountryRepository : RepositoryS<Country>, ICountryRepository
    {
        //Constructor
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
        }

        //Methods
        public async Task<Country> GetByName(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task<Country> GetByPhoneCode(string code)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.PhoneNumberCode == code);
        }
    }
}
