using EmailProvider.Models.DBModels;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDbContext _context;

        public LanguageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            return await _context.Languages
                .AsNoTracking()
                .OrderBy(l => l.Name)
                .ToListAsync();
        }

        public async Task<Language?> GetByIdAsync(int id)
        {
            return await _context.Languages
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Languages.AnyAsync(l => l.Id == id);
        }
    }
}
