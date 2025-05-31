using EmailProvider.Enums;
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
    public class FolderRepository : IFolderRepository
    {
        private readonly ApplicationDbContext _context;

        public FolderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Folder>> GetFoldersForUserAsync(int userId)
        {
            return await _context.Folders
                .Where(f => f.UserId == userId)
                .OrderBy(f => f.Name)
                .ToListAsync();
        }

        public async Task<Folder?> GetByIdAsync(int folderId)
        {
            return await _context.Folders.FindAsync(folderId);
        }

        public async Task AddAsync(Folder folder)
        {
            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Folder folder)
        {
            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
        }
    }
}
