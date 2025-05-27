using EmailService;

var smtpServer = new SmtpServer(2525); // Use 2525 for local testing
await smtpServer.StartAsync();