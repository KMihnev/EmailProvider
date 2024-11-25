using EmailProvider.Dispatches;
using EmailProvider.Enums;
using EmailProviderServer.DBContext;
using EmailProviderServer.DBContext.Repositories;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server.Dispatches.Countries;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;

namespace EmailProviderServer.TCP_Server.Dispatches
{
    public class DispatchMapper
    {
        private readonly ApplicationDbContext _context;

        public DispatchMapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public BaseDispatchHandler MapDispatch(int dispatchCode)
        {
            switch ((DispatchEnums)dispatchCode)
            {
                case DispatchEnums.Register:
                    return new RegisterHandler(new UserService(new UserRepository(_context)));
                case DispatchEnums.SetUpProfile:
                    return new SetUpProfileDispatch(new UserService(new UserRepository(_context)));
                case DispatchEnums.GetCountries:
                    return new GetCountriesDispatch(new CountryService(new CountryRepository(_context)));
                default:
                    return null;
            }
        }
    }
}
