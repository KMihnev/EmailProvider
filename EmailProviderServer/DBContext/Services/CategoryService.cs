//Includes
using EmailProviderServer.DBContext.Repositories.Base;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services
{
    //------------------------------------------------------
    //	CategoryService
    //------------------------------------------------------

    public class CategoryService : IFolderService
    {
        private readonly IRepositoryS<Folder> oCategoryRepositoryS;

        //Constructor
        public CategoryService(IRepositoryS<Folder> oCategoryRepository)
        {
            this.oCategoryRepositoryS = oCategoryRepository;
        }

        //Methods
        public IEnumerable<Folder> GetAll(int? nCount = null)
        {
            IQueryable<Folder> oQuery = this.oCategoryRepositoryS
                .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public Folder GetById(int nId)
        {
            var oCategory = this.oCategoryRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .FirstOrDefault();
            return oCategory;
        }

        public Folder GetByName(string strName)
        {
            var oCategory = this.oCategoryRepositoryS
                .All()
               .Where(x => x.Name == strName)
               .FirstOrDefault();
            return oCategory;
        }
    }
}
