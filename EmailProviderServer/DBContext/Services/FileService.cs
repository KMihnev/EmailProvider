//Includes
using EmailProviderServer.DBContext.Repositories.Base;
using EmailProviderServer.DBContext.Services.Base;

namespace EmailProviderServer.DBContext.Services
{
    //------------------------------------------------------
    //	FileService
    //------------------------------------------------------
    public class FileService : IFileService
    {
        private readonly IRepositoryS<EmailServiceIntermediate.Models.File> oFileRepositoryS;

        //Constructor
        public FileService(IRepositoryS<EmailServiceIntermediate.Models.File> oFileRepository)
        {
            this.oFileRepositoryS = oFileRepository;
        }

        //Methods
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
