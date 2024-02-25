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

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        //Задаваме контекс
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

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


