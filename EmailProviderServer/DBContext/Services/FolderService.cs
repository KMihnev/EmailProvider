//Includes
using AutoMapper;
using EmailProvider.Enums;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Logging;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailProviderServer.DBContext.Services
{
    //------------------------------------------------------
    //	CountryService
    //------------------------------------------------------
    public class FolderService : IFolderService
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public FolderService(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }

        public async Task<List<T>> GetUserFoldersAsync<T>(int userId)
        {
            var folders = await _folderRepository.GetFoldersForUserAsync(userId);
            return _mapper.Map<List<T>>(folders);
        }

        public async Task<T?> GetFolderByIdAsync<T>(int folderId)
        {
            var folder = await _folderRepository.GetByIdAsync(folderId);
            return folder == null ? default : _mapper.Map<T>(folder);
        }

        public async Task CreateFolderAsync<T>(T folderDto)
        {
            var entity = _mapper.Map<Folder>(folderDto);
            await _folderRepository.AddAsync(entity);
        }

        public async Task DeleteFolderAsync(int folderId)
        {
            var folder = await _folderRepository.GetByIdAsync(folderId);
            if (folder != null)
            {
                await _folderRepository.DeleteAsync(folder);
            }
        }
    }
}
