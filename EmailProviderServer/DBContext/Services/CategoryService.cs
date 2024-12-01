//Includes

using EmailProviderServer.DBContext.Repositories.Base;
using EmailProviderServer.DBContext.Services.Base;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryS<Category> oCategoryRepositoryS;

        public CategoryService(IRepositoryS<Category> oCategoryRepository)
        {
            this.oCategoryRepositoryS = oCategoryRepository;
        }

        public IEnumerable<Category> GetAll(int? nCount = null)
        {
            IQueryable<Category> oQuery = this.oCategoryRepositoryS
                .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.ToList();
        }

        public Category GetById(int nId)
        {
            var oCategory = this.oCategoryRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .FirstOrDefault();
            return oCategory;
        }

        public Category GetByName(string strName)
        {
            var oCategory = this.oCategoryRepositoryS
                .All()
               .Where(x => x.Name == strName)
               .FirstOrDefault();
            return oCategory;
        }
    }
}
