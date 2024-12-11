using EmailProviderServer.DBContext.Repositories.Interfaces;

namespace EmailProviderServer.DBContext.Repositories
{
    //------------------------------------------------------
    //	FileRepository
    //------------------------------------------------------
    public class FileRepository : RepositoryS<EmailServiceIntermediate.Models.File>, IFileRepository
    {
        //Constructor
        public FileRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
