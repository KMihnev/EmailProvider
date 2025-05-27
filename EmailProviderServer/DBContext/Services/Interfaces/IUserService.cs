//Includes


using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    //------------------------------------------------------
    //	IUserService
    //------------------------------------------------------
    public interface IUserService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetByEmailAsync<T>(string email);

        Task<T> GetByIdAsync<T>(int id);

        Task<T> GetByNameAsync<T>(string name);

        Task<bool> CheckIfExistsAsync(int id);

        Task<bool> CheckIfExistsEmailAsync(string email);

        Task<T> CreateAsync<T>(User user);

        Task<T> UpdateAsync<T>(User user);
    }
}
