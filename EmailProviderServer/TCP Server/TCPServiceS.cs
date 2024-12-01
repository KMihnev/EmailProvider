﻿using Microsoft.Extensions.Hosting;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using EmailProviderServer.TCP_Server.Dispatches;
using EmailProviderServer.DBContext;
using EmailProviderServer.TCP_Server.Dispatches.Interfaces;
using EmailServiceIntermediate.Dispatches;
using EmailServiceIntermediate.Logging;
using AutoMapper;

namespace EmailProviderServer.TCP_Server
{
    public class TcpServerService : BackgroundService
    {
        private readonly TcpListener _listener;
        private readonly DispatchMapper _dispatchMapper;

        public TcpServerService(TcpListener listener, DispatchMapper dispatchMapper)
        {
            _listener = listener;
            _dispatchMapper = dispatchMapper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listener.Start();
            _listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            Console.WriteLine("TCP Server started.");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var client = await _listener.AcceptTcpClientAsync(stoppingToken);
                    client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                    Console.WriteLine("Client connected.");
                    _ = HandleClientAsync(client, stoppingToken);
                }
            }
            finally
            {
                _listener.Stop();
            }
        }

        private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
        {
            using (client)
            {
                var stream = client.GetStream();

                SmartStreamArray InPackage = new SmartStreamArray();
                SmartStreamArray OutPackage = new SmartStreamArray();

                try
                {
                    // Зареждаме потока в "умниия масив" ;)
                    InPackage.LoadFromStream(stream);

                    // Десериализираме си кода на диспача
                    InPackage.Deserialize(out int dispatchCode);

                    BaseDispatchHandler dispatchHandler = _dispatchMapper.MapDispatch(dispatchCode);

                    if (!await dispatchHandler?.Execute(InPackage, OutPackage))
                    {
                        SetResponseFailed(OutPackage, dispatchHandler.errorMessage);
                    }

                    // изпращаме отговор
                    byte[] responseData = OutPackage.ToByte();
                    await stream.WriteAsync(responseData, 0, responseData.Length, cancellationToken);

                    await stream.FlushAsync();
                }
                catch (Exception ex)
                {
                    Logger.LogError(LogMessages.InteralError);
                    SetResponseFailed(OutPackage);
                }
            }
        }

        private void SetResponseFailed(SmartStreamArray ResponsePackage, string error = "")
        {
            ResponsePackage.Serialize(false);
            ResponsePackage.Serialize(error);
        }
    }
}
