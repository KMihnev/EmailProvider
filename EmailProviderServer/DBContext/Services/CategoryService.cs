//Includes

using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailProviderServer.Models;
using EmailServiceIntermediate.Mapping;

namespace EmailProviderServer.DBContext.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryS<Category> oCategoryRepositoryS;

        public CategoryService(IRepositoryS<Category> oCategoryRepository)
        {
            this.oCategoryRepositoryS = oCategoryRepository;
        }

        public IEnumerable<T> GetAll<T>(int? nCount = null)
        {
            IQueryable<Category> oQuery = this.oCategoryRepositoryS
                .All();

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByUserId<T>(int nId, int? nCount = null)
        {
            IQueryable<Category> oQuery = this.oCategoryRepositoryS
                .All().Where(c => c.UserId == nId);

            if (nCount.HasValue)
                oQuery = oQuery.Take(nCount.Value);

            return oQuery.To<T>().ToList();
        }

        public T GetById<T>(int nId)
        {
            var oCategory = this.oCategoryRepositoryS
                .All()
                .Where(x => x.Id == nId)
                .To<T>().FirstOrDefault();
            return oCategory;
        }

        public T GetByName<T>(string strName)
        {
            var oCategory = this.oCategoryRepositoryS
                .All()
               .Where(x => x.Name == strName)
               .To<T>().FirstOrDefault();
            return oCategory;
        }
    }
}
