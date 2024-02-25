using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using EmailProviderServer.DBContext;
using EmailProviderServer.DBContext.Services.Base;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        //Задаваме контекс
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

        //Базово Repository
        services.AddScoped(typeof(IRepositoryS<>), typeof(RepositoryS<>));

        services.AddTransient<, SettingsService>();
        services.AddTransient<IEventsService, EventsService>();
        services.AddTransient<IUsersService, UsersService>();

    })
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        // Configure additional sources for app settings if needed
        // e.g., configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    })
    .Build();

// Application starting point
await host.RunAsync();


