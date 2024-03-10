//Includes
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using EmailProviderServer.DBContext;
using EmailProviderServer.DBContext.Services.Interfaces.Base;
using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.DBContext.Services;
using EmailProviderServer.TCP_Server;
using EmailProvider.Settings;
using System.Net.Sockets;
using System.Net;
using EmailServiceIntermediate.Models;
using AutoMapper;
using System.Reflection;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        IniReader.InitIni(@"E:\EmailDomain\EmailProvider\Executables\Settings.ini");

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
        services.AddTransient<IIncomingMessageService, IncomingMessageService>();
        services.AddTransient<IOutgoingMessageService, OutgoingMessageService>();
        services.AddTransient<IUserService, UserService>();

        services.AddSingleton<TcpListener>(provider => new TcpListener(IPAddress.Parse("192.168.1.27"), 8009));
        services.AddHostedService<TcpServerService>();
    })
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        // Configure additional sources for app settings if needed
        // e.g., configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    })
    .Build();

// Application starting point
await host.RunAsync();