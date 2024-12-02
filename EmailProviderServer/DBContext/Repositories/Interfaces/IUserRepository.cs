using EmailProviderServer.DBContext.Repositories.Base;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryS<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByName(string name);

        Task<bool> CheckIfExistsEmail(string email);
    }
}
