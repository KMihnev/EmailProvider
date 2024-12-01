//Includes

using EmailProviderServer.DBContext.Repositories.Base;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services
{
    public class FileService : IFileService
    {
        private readonly IRepositoryS<EmailServiceIntermediate.Models.File> oFileRepositoryS;

        public FileService(IRepositoryS<EmailServiceIntermediate.Models.File> oFileRepository)
        {
            this.oFileRepositoryS = oFileRepository;
        }

        public IEnumerable<EmailServiceIntermediate.Models.File> GetAll(int? nCount = null)
        {
            IQueryable<EmailServiceIntermediate.Models.File> oQuery = this.oFileRepositoryS
                .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public EmailServiceIntermediate.Models.File GetById(int nId)
        {
            var oFile = this.oFileRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .FirstOrDefault();
            return oFile;
        }
    }
}
