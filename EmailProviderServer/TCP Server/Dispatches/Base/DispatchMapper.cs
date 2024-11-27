using AutoMapper;
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

        private readonly IMapper _mapper;

        public DispatchMapper(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BaseDispatchHandler MapDispatch(int dispatchCode)
        {
            switch ((DispatchEnums)dispatchCode)
            {
                case DispatchEnums.Register:
                    return new RegisterDispatch(new UserService(new UserRepository(_context), _mapper));
                case DispatchEnums.SetUpProfile:
                    return new SetUpProfileDispatch(new UserService(new UserRepository(_context), _mapper));
                case DispatchEnums.GetCountries:
                    return new GetCountriesDispatch(new CountryService(new CountryRepository(_context), _mapper));
                case DispatchEnums.Login:
                    return new LoginDispatch(new UserService(new UserRepository(_context), _mapper));
                default:
                    return null;
            }
        }
    }
}
