//Includes

using EmailProvider.Dispatches;
using EmailProvider.Enums;
using EmailProviderServer.DBContext;
using EmailProviderServer.DBContext.Repositories;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using System.Text.Json;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class DispatchMapper
    {
        private readonly ApplicationDbContext _context;

        public DispatchMapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public IDispatchHandler MapDispatch(MethodRequest request)
        {
            switch (request.eDispatch)
            {
                case DispatchEnums.Register:
                    {
                        UserService userService = new UserService(new UserRepository(_context));
                        RegisterHandler registerHandler = new RegisterHandler(userService);
                        return registerHandler;
                    } // case
                default:
                    {
                        return null;
                    }
            } //switch
        }
    }
}
