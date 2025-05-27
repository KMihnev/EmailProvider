using EmailProviderServer.DBContext.Repositories.Base;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    //------------------------------------------------------
    //	IFileRepository
    //------------------------------------------------------
    public interface IFileRepository : IRepositoryS<EmailServiceIntermediate.Models.File>
    {
        void Delete(EmailServiceIntermediate.Models.File entity);
    }
}
