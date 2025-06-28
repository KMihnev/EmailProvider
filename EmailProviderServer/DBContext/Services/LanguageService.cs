using AutoMapper;
using EmailProvider.Models.DBModels;
using EmailProvider.Models.Serializables;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProviderServer.DBContext.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _repository;
        private readonly IMapper _mapper;

        public LanguageService(ILanguageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var languages = await _repository.GetAllLanguagesAsync();
            return _mapper.Map<IEnumerable<T>>(languages);
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var language = await _repository.GetByIdAsync(id);
            return _mapper.Map<T>(language);
        }

        public async Task<bool> IsValidLanguageIdAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}
