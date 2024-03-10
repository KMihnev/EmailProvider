//Includes

namespace EmailProviderServer.DBContext.Services.Base
{
    public interface IFileService
    {
        IEnumerable<EmailServiceIntermediate.Models.File> GetAll(int? nCount = null);

        EmailServiceIntermediate.Models.File GetById(int nId);
    }
}
