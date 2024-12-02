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
    public class CountryRepository : RepositoryS<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
        }

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
