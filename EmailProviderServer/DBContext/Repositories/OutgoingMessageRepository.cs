using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories
{
    public class OutgoingMessageRepository : RepositoryS<OutgoingMessage>, IOutgoingMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public OutgoingMessageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
