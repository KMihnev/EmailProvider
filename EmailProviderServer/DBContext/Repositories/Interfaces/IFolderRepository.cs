using EmailProvider.Enums;
using EmailProviderServer.DBContext.Repositories.Base;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    public interface IFolderRepository
    {
        Task<List<Folder>> GetFoldersForUserAsync(int userId);
        Task<Folder?> GetByIdAsync(int folderId);
        Task AddAsync(Folder folder);
        Task DeleteAsync(Folder folder);
    }
}
