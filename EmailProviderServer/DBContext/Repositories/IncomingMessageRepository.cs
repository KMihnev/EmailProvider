//Includes
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Repositories
{
    //------------------------------------------------------
    //	IncomingMessageRepository
    //------------------------------------------------------
    public class IncomingMessageRepository : RepositoryS<IncomingMessage>, IIncomingMessageRepository
    {
        //Constructor
        public IncomingMessageRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
