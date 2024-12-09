//Includes
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Repositories
{
    //------------------------------------------------------
    //	InnerMessageRepository
    //------------------------------------------------------
    public class InnerMessageRepository : RepositoryS<InnerMessage>, IInnerMessageRepository
    {
        //Constructor
        public InnerMessageRepository(ApplicationDbContext context) : base(context)
        {

        }

    }
}
