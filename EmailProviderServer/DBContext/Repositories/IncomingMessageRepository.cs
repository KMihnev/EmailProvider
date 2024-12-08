using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories
{
    internal class IncomingMessageRepository : RepositoryS<IncomingMessage>, IIncomingMessageRepository
    {
        public IncomingMessageRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
