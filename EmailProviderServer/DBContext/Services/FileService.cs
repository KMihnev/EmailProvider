//Includes

using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailProviderServer.Models;
using EmailServiceIntermediate.Mapping;

namespace EmailProviderServer.DBContext.Services
{
    public class FileService : IFileService
    {
        private readonly IRepositoryS<Models.File> oFileRepositoryS;

        public FileService(IRepositoryS<Models.File> oFileRepository)
        {
            this.oFileRepositoryS = oFileRepository;
        }

        public IEnumerable<T> GetAll<T>(int? nCount = null)
        {
            IQueryable<Models.File> oQuery = this.oFileRepositoryS
                .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.To<T>().ToList();
        }

        public T GetById<T>(int nId)
        {
            var oFile = this.oFileRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .To<T>().FirstOrDefault();
            return oFile;
        }
    }
}
