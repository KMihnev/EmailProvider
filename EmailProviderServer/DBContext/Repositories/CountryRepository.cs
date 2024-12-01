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
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Country> All()
        {
            return _context.Countries;
        }

        public IQueryable<Country> GetByID(int nId)
        {
            return _context.Countries.Where(x => x.Id == nId);
        }

        public IQueryable<Country> AllAsNoTracking()
        {
            return _context.Countries.AsNoTracking();
        }

        public async Task AddAsync(Country entity)
        {
            await _context.Countries.AddAsync(entity);
        }

        public void Update(Country entity)
        {
            _context.Countries.Update(entity);
        }

        public void Delete(Country entity)
        {
            _context.Countries.Remove(entity);
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
