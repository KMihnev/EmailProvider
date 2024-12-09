//Includes
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Repositories
{
    //------------------------------------------------------
    //	OutgoingMessageRepository
    //------------------------------------------------------
    public class OutgoingMessageRepository : RepositoryS<OutgoingMessage>, IOutgoingMessageRepository
    {
        private readonly ApplicationDbContext _context;

        //Constructor
        public OutgoingMessageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
