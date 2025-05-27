//Includes
using EmailProviderServer.DBContext.Repositories.Base;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    //------------------------------------------------------
    //	IUserRepository
    //------------------------------------------------------
    public interface IUserRepository : IRepositoryS<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByName(string name);

        Task<bool> CheckIfExistsEmail(string email);
    }
}
