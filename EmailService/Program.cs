using EmailService;
using EmailService.PrivateService;
using EmailService.PublicService;

var publicTask = new SmtpPublicServer().StartAsync();
var privateTask = new SmtpPrivateServer().StartAsync();

await Task.WhenAll(publicTask, privateTask);