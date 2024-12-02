using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories
{
    public class InnerMessageRepository : RepositoryS<InnerMessage>, IInnerMessageRepository
    {
        public InnerMessageRepository(ApplicationDbContext context) : base(context)
        {

        }

    }
}
