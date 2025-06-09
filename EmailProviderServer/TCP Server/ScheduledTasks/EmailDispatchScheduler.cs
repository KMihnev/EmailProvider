using EmailProviderServer.DBContext.Services.Base;
using EmailProviderServer.TCP_Server.ServiceDispatches;
using EmailServiceIntermediate.Models.Serializables;
using Microsoft.Extensions.DependencyInjection;
namespace EmailProviderServer.TCP_Server.ScheduledTasks
{

    public class EmailDispatchScheduler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(5);
        private Timer _timer;

        public EmailDispatchScheduler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Start()
        {
            _timer = new Timer(async _ => await DispatchEmailsAsync(), null, TimeSpan.Zero, _interval);
        }

        private async Task DispatchEmailsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var messageService = scope.ServiceProvider.GetRequiredService<IMessageService>();

            List<EmailViewModel> messages = await messageService.GetMessagesForSending(100);

            foreach (var message in messages)
            {
                var EmailServiceDispatch = new SendEmailServiceDispatchS(messageService);
                bool sent = await EmailServiceDispatch.SendEmail(message);
                if (sent)
                {
                    Console.WriteLine("Sending Complete");
                }
            }
        }
    }
}
