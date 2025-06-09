//Includes
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmailProviderServer.DBContext;
using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server;
using System.Net.Sockets;
using System.Net;
using EmailServiceIntermediate.Settings;
using EmailProviderServer.TCP_Server.Dispatches;
using EmailProviderServer.DBContext.Repositories.Base;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailProviderServer.DBContext.Repositories;
using EmailProviderServer.DBContext.Services.Interfaces;
using EmailProviderServer.TCP_Server.ScheduledTasks;

void AddServices(IServiceCollection services)
{
    //Задаваме контекс
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(ConnectionStringCreator.CreateConnectionString()));

    //Базово Repository
    services.AddScoped(typeof(IRepositoryS<>), typeof(RepositoryS<>));

    //Регистирране на repositories

    services.AddScoped<ICountryRepository, CountryRepository>();
    services.AddScoped<IUserMessageRepository, UserMessageRepository>();
    services.AddScoped<IMessageRepository, MessageRepository>();
    services.AddScoped<IFileRepository, FileRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IFolderRepository, FolderRepository>();
    services.AddScoped<IBulkIncomingMessagesRepositoryS, BulkIncomingMessagesRepository>();
    services.AddScoped<IBulkOutgoingMessagesRepositoryS, BulkOutgoingMessagesRepository>();

    //Регистриране на сервизи
    services.AddScoped<IBulkIncomingMessageService, BulkIncomingMessageService>();
    services.AddScoped<IBulkOutgoingMessageService, BulkOutgoingMessageService>();
    services.AddScoped<IUserMessageService, UserMessageService>();
    services.AddScoped<ICountryService, CountryService>();
    services.AddScoped<IMessageService, MessageService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IFolderService, FolderService>();

    services.AddSingleton<EmailDispatchScheduler>();
    services.AddHostedService<EmailSchedulerHostedService>();
    services.AddSingleton<DispatchMapper>();
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