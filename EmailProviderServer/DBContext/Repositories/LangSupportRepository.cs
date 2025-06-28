using EmailProviderServer.DBContext.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories
{
    internal class LangSupportRepository : ILangSupportRepository
    {
        private readonly ApplicationDbContext _context;

        public LangSupportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetLocalizedStringsAsync(int languageId)
        {
            var list = await _context.LangSupports
                .Where(ls => ls.LanguageId == languageId)
                .ToListAsync();

            return list
                .GroupBy(x => x.MessageName.Trim(), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First().Value, StringComparer.OrdinalIgnoreCase);
        }
    }
}
