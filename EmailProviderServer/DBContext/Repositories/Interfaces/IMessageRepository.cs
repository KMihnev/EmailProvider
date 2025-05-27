//Includes
using EmailProviderServer.DBContext.Repositories.Base;
using EmailServiceIntermediate.Models;
using System.Linq.Expressions;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    //------------------------------------------------------
    //	IMessageRepository
    //------------------------------------------------------
    public interface IMessageRepository : IRepositoryS<Message>
    {
        Task<Message> GetByIDIncludingAll(int nId);

        Task<List<Message>> GetByIDsIncludingAll(IEnumerable<int> ids);
    }
}
