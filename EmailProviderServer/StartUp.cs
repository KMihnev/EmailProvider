//Includes
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmailProviderServer.DBContext;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server;
using System.Net.Sockets;
using System.Net;
using EmailServiceIntermediate.Settings;
using EmailProviderServer.TCP_Server.Dispatches;

void AddServices(IServiceCollection services)
{
    //Задаваме контекс
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(ConnectionStringCreator.CreateConnectionString()));

    //Базово Repository
    services.AddScoped(typeof(IRepositoryS<>), typeof(RepositoryS<>));

    //Регистриране на сервизи
    services.AddTransient<IBulkIncomingMessageService, BulkIncomingMessageService>();
    services.AddTransient<IBulkOutgoingMessageService, BulkOutgoingMessageService>();
    services.AddTransient<ICategoryService, CategoryService>();
    services.AddTransient<ICountryService, CountryService>();
    services.AddTransient<IFileService, FileService>();
    services.AddTransient<IMessageService, MessageService>();
    services.AddTransient<IUserService, UserService>();
}

void LoadAutoMapperProfiles(IServiceCollection services)
{
    services.AddAutoMapper(cfg =>
    {
        cfg.AddProfile<EmailServiceIntermediate.AutoMapper.Profiles.ModelsProfile>();
    });
}

void RegisterTcpServer(IServiceCollection services)
{
    IPAddress ipAddress = IPAddress.Parse(SettingsProvider.GetServerIP());
    int port = int.Parse(SettingsProvider.GetServerPort());

    services.AddSingleton<TcpListener>(provider => new TcpListener(ipAddress, port));
    services.AddHostedService<TcpServerService>();
}

IHost GetDefaultHost()
{
    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        RegisterTcpServer(services);
        AddServices(services);
        LoadAutoMapperProfiles(services);

    })
    .Build();

    return host;
}

var host = GetDefaultHost();
await host.RunAsync();