using AutoMapper;
using EmailService;
using EmailService.PrivateService;
using EmailService.PublicService;
using EmailServiceIntermediate.Settings;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;

X509Certificate2 serverCert = null;
try
{
    serverCert = new X509Certificate2(SettingsProvider.GetSMTPServicePublicCertPFXPath(), SettingsProvider.GetSMTPServicePublicCertPassword(), X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}
var publicTask = new SmtpPublicServer().StartAsync();
var privateTask = new SmtpPrivateServer(serverCert).StartAsync();
await Task.WhenAll(publicTask, privateTask);
