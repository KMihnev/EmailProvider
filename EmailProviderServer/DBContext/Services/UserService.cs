//Includes
using AutoMapper;
using EmailServiceIntermediate.Logging;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailProviderServer.DBContext.Services
{
    //------------------------------------------------------
    //	UserService
    //------------------------------------------------------
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        //Constructor
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        //Methods
        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var users = await _userRepository.AllAsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<T>>(users);
        }

        public async Task<T> GetByEmailAsync<T>(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

            return _mapper.Map<T>(user)!;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var user = await _userRepository.GetByID(id);
            return _mapper.Map<T>(user);
        }

        public async Task<T> GetByNameAsync<T>(string name)
        {
            var user = await _userRepository.GetUserByName(name);

            return _mapper.Map<T>(user);
        }

        public async Task<bool> CheckIfExistsAsync(int id)
        {
            return await _userRepository.CheckIfExists(id);
        }

        public async Task<bool> CheckIfExistsEmailAsync(string email)
        {
            return await _userRepository.CheckIfExistsEmail(email);
        }

        public async Task<T> CreateAsync<T>(User user)
        {
            if (user == null)
                Logger.LogNullValue();

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<T>(user);
        }

        public async Task<T> UpdateAsync<T>(User user)
        {
            if (user == null)
            {
                Logger.LogNullValue();
                throw new ArgumentNullException(nameof(user));
            }

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<T>(user);
        }
    }
}
